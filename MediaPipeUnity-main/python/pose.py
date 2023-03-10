import socket
import cv2
import mediapipe as mp
mp_drawing = mp.solutions.drawing_utils
mp_drawing_styles = mp.solutions.drawing_styles
mp_pose = mp.solutions.pose

UDP_IP = "127.0.0.1"
UDP_PORT = 21901

sock = socket.socket(socket.AF_INET, #Internet
                     socket.SOCK_DGRAM) #UDP

# For webcam input:
cap = cv2.VideoCapture(0)
with mp_pose.Pose(
    min_detection_confidence=0.5,
    min_tracking_confidence=0.5) as pose:
  while cap.isOpened():
    success, image = cap.read()
    if not success:
      print("Ignoring empty camera frame.")
      # If loading a video, use 'break' instead of 'continue'.
      continue

    # To improve performance, optionally mark the image as not writeable to
    # pass by reference.
    image.flags.writeable = False
    #image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
    image = cv2.cvtColor(cv2.flip(image, 1), cv2.COLOR_BGR2RGB)
    results = pose.process(image)

    #Send landmarks using UDP
    detections = results.pose_landmarks

    if detections is not None:
        msg = ""
        for landmark in detections.landmark:
            msg+=str(round(landmark.x,3))+','+str(round(landmark.y,3))+','+str(round(landmark.z,3))+','+str(landmark.visibility)+','
        sockmsg=bytearray(msg, 'utf-8')
        sock.sendto(sockmsg, (UDP_IP,UDP_PORT))
        #print(msg)

    # Draw the pose annotation on the image.
    image.flags.writeable = True
    image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)
    mp_drawing.draw_landmarks(
        image,
        results.pose_landmarks,
        mp_pose.POSE_CONNECTIONS,
        landmark_drawing_spec=mp_drawing_styles.get_default_pose_landmarks_style())
    # Flip the image horizontally for a selfie-view display.
    cv2.imshow('MediaPipe Pose', image)
    if cv2.waitKey(5) & 0xFF == 27:
      break

cap.release()
