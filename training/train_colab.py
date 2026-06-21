!pip install -q ultralytics
from google.colab import files
import zipfile, os, glob
print(">>> Выбери файл dataset_yolo.zip")
image_up = files.upload()
zip_name = list(image_up.keys())[0]
zipfile.ZipFile(zip_name).extractall("/content/ds")
yaml_path = glob.glob("/content/ds/**/data.yaml", recursive=True)[0]
root = os.path.dirname(yaml_path)
open(yaml_path, "w").write("path: " + root + "\ntrain: images/train\nval: images/val\nnames:\n  0: mob\n")
print(">>> Dataset:", root, "| train:", len(glob.glob(root+"/images/train/*")), "| val:", len(glob.glob(root+"/images/val/*")))
from ultralytics import YOLO
model = YOLO("yolov8n.pt")
model.train(data=yaml_path, epochs=80, imgsz=640, batch=16, patience=20, degrees=5, scale=0.5, project="/content/runs", name="mobs")
YOLO("/content/runs/mobs/weights/best.pt").export(format="onnx", opset=12, imgsz=640, simplify=True)
os.rename("/content/runs/mobs/weights/best.onnx", "/content/mobs.onnx")
print(">>> Done! Downloading mobs.onnx")
files.download("/content/mobs.onnx")




  import glob, os
  from ultralytics import YOLO
  from google.colab import files

  cands = glob.glob("/content/runs/**/weights/best.pt", recursive=True)
  best = max(cands, key=os.path.getmtime)
  print(">>> Беру модель:", best)

  YOLO(best).export(format="onnx", opset=12, imgsz=640, simplify=True)
  onnx = best.replace("best.pt", "best.onnx")
  os.rename(onnx, "/content/mobs.onnx")
  print(">>> Готово, скачиваю mobs.onnx")
  files.download("/content/mobs.onnx")
