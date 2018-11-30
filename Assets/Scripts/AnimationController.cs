using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

    private Animator animator;

    //Character Animation State Hashes
    private readonly int kickHash = Animator.StringToHash("RoundKick");
    private readonly int idleHash = Animator.StringToHash("Idle");
    private readonly int happyHash = Animator.StringToHash("Smile");
    private readonly int cryHash = Animator.StringToHash("Cry");
    private readonly int frownHash = Animator.StringToHash("Frown");
    private readonly int meanHash = Animator.StringToHash("Mean");

    private DebugWriter debugWriter;
    private Dictionary<string, float> recommendedValues;

    void Start ()
    {
        animator = GetComponent<Animator>();
        debugWriter = FindObjectOfType<DebugWriter>();
        recommendedValues = new Dictionary<string, float>
        {
            { "Calories", 2000f },
            { "Fat", 65f },
            { "Protein", 50f },
            { "DietaryFiber", 25f }
        };
    }

    public float GetRecommendedValue(string key)
    {
        if(!recommendedValues.ContainsKey(key))
        {
            throw new System.Exception("Invalid Nutrient");
        }

        return recommendedValues[key];
    }

    public void UpdateCharacterAnimation()
    {
        float Calories = PlayerPrefs.GetFloat("currentCalories");
        float TotalFat = PlayerPrefs.GetFloat("currentTotalFat");
        float Protein = PlayerPrefs.GetFloat("currentProtein");
        float DietaryFiber = PlayerPrefs.GetFloat("currentDietaryFiber");

        string[] foods = PlayerPrefs.GetString("Foods").Split(' ');

        string log = "";

        if (Calories > 0)
        {
            log = "Calories : " + Calories + "\n TotalFat : " + TotalFat + "\n Protein : " + Protein + "\n Dietary Fiber : " + DietaryFiber;
            Debug.Log(log);
            debugWriter.WriteToFile(log);
        }

        if (foods.Length > 0)
        {
            log = "Foods Detected : " + string.Join(",", foods);
            debugWriter.WriteToFile(log);
            Debug.Log(log);
        }

        /*Calories: 107.1
        TotalFat: 2.64
        Protein: 8.93
        Dietary Fiber : 0*/

        /*
         * For Fattening - scale X
         * For Making tall - Scale Y
         * For Thinning - Decrease X, Z
         */

        float playerCalories = PlayerPrefs.GetFloat("totalCalories");
        float playerFat = PlayerPrefs.GetFloat("totalFat");
        float playerProtein = PlayerPrefs.GetFloat("totalProtein");
        float playerFiber = PlayerPrefs.GetFloat("totalDietaryFiber");

        if (Calories == 0)
        {
            Debug.Log("Idle True");
            animator.SetTrigger(idleHash);
        }

        if (GetRecommendedValue("Calories") == 0)
        {
            throw new System.Exception("chutyapa");
        }

        if ((Calories + playerCalories) > 0)
        {
            if (Calories + playerCalories <= GetRecommendedValue("Calories"))
            {
                animator.SetTrigger(kickHash);

                StartCoroutine(WaitForCompletion());

                if (TotalFat + playerFat <= GetRecommendedValue("Fat"))
                {
                    Debug.Log("Fat True");
                    transform.localScale = new Vector3((transform.localScale.x + 0.05f), transform.localScale.y, transform.localScale.z);
                    animator.SetTrigger(happyHash);
                }
                else
                {
                    Debug.Log("Fat False");
                    transform.localScale = new Vector3(0.5f, transform.localScale.y, transform.localScale.z);
                    animator.SetTrigger(frownHash);
                }

                StartCoroutine(WaitForSeconds());

                if (Protein + playerProtein <= GetRecommendedValue("Protein"))
                {
                    Debug.Log("Protein True");
                    transform.localScale = new Vector3(transform.localScale.x + 0.1f, transform.localScale.y + 0.2f, transform.localScale.z);
                    animator.SetTrigger(happyHash);
                }

                StartCoroutine(WaitForSeconds());

                if (DietaryFiber + playerFiber <= GetRecommendedValue("DietaryFiber"))
                {
                    Debug.Log("Fiber True");
                    transform.localScale = new Vector3(transform.localScale.x - 0.05f, transform.localScale.y, transform.localScale.z - 0.05f);
                    animator.SetTrigger(happyHash);
                }
                else
                {
                    Debug.Log("Fiber False");
                    transform.localScale = new Vector3(0.5f, transform.localScale.y, transform.localScale.z);
                    animator.SetTrigger(meanHash);
                }

                StartCoroutine(WaitForSeconds());
            }
            else
            {
                Debug.Log("Calorie False");
                transform.localScale = new Vector3(0.5f, transform.localScale.y, 0.5f);
                animator.SetTrigger(cryHash);
            }

            PlayerPrefs.SetFloat("totalCalories", Calories + playerCalories);
            PlayerPrefs.SetFloat("totalProtein", Protein + playerProtein);
            PlayerPrefs.SetFloat("totalFat", TotalFat + playerFat);
            PlayerPrefs.SetFloat("totalDietaryFiber", DietaryFiber + playerFiber);
        }
    }

    private IEnumerator WaitForCompletion()
    {
        float time = animator.GetCurrentAnimatorStateInfo(0).length + animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        Debug.Log("TIME : " + time);
        yield return new WaitForSeconds(time);
    }

    private IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(3f);
    }
}
