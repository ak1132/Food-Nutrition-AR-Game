<<<<<<< HEAD
function in tf.py
parameter: content is a byte array image
dependencies: 
	package: numpy, tensorflow, sklearn, opencv-python, google-cloud-vision
	files: GoogleCloud.json (to call the API, export GOOGLE_APPLICATION_CREDENTIALS="[path]/GoogleCloud.json" in bash before running)
	bvlc_alexnet.npy (network weights), caffe_classes.py, namedict.json (food classes), svm.pkl (trained classifier)
=======
function in tf.py
parameter: content is a byte array image
dependencies: 
	package: numpy, tensorflow, sklearn, opencv-python, google-cloud-vision
	files: GoogleCloud.json (to call the API, export GOOGLE_APPLICATION_CREDENTIALS="[path]/GoogleCloud.json" in bash before running)
	bvlc_alexnet.npy (network weights), caffe_classes.py, namedict.json (food classes), svm.pkl (trained classifier)
>>>>>>> final neural-network code
	in same directory as tf.py