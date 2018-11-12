import requests
import json
import os

url = 'https://trackapi.nutritionix.com/v2/natural/nutrients'
headers = {'Content-Type': 'application/json',
           "x-app-id": "20cc37ae", 'x-app-key': 'b38f89ed44a520302234f5b83853b4c9', 'x-remote-user-id': '0'}


data = '{ \"num_servings\": 1,\"query\": \"mango\",  \"aggregate\": \"string\",  \"line_delimited\": false,  \"use_raw_foods\": false,  \"include_subrecipe\": false,  \"timezone\": \"US\/ Eastern\",  \"consumed_at\": null,  \"lat\": 0,  \"lng\": 0,  \"meal_type\": 0,  \"use_branded_foods\": false,  \"locale\": \"en_US\"}'

json_data = json.loads(data)

# way to change query
json_data["query"] = "apple"

print("Request:\n\n"+json.dumps(json_data, indent=2, sort_keys=True))

response = requests.post(url, data=data, headers=headers)

# could write this to a file
parsed = json.loads(response.text)
# print(parsed["foods"][0]["full_nutrients"])

#attr = parsed["foods"][0]["full_nutrients"]
# print(attr)
# for i in range(0, len(attr)):
#    print(attr[i]+"\n")

response_json = json.dumps(parsed, indent=2, sort_keys=True)
# print("\n\nResponse:\n\n"+response_json)
