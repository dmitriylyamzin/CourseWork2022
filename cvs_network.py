# -*- coding: utf-8 -*-
"""Untitled14.ipynb

Automatically generated by Colaboratory.

Original file is located at
    https://colab.research.google.com/drive/1hc1TkF2RUDoIZO1ORqCcS9p8szgb7_pa
"""

import tensorflow
import keras
import keras.layers
import tensorflow.keras.layers
import pandas as pd
from sklearn.model_selection import train_test_split
from tensorflow.keras.layers import Dense

dataset = pd.read_csv('/content/CVS_dataset.csv', delimiter = ';')
y = dataset[dataset.columns[226]]
X = dataset.drop (dataset.columns[226], axis = 1)
X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.3)

model = keras.Sequential([Dense(64,input_shape=(226,)),
 Dense(64, activation='relu'),
 Dense(64, activation='relu'),
 Dense(16, activation='relu'),
 Dense(1, activation='sigmoid')])
print(model.summary())
model.compile(optimizer='adam',
loss='mae',
metrics=['mae'])
model.fit(X_train, y_train, epochs=100)
model.evaluate(X_test, y_test)

!pip install vk_api

import vk_api
from vk_api.longpoll import VkLongPoll, VkEventType

def find(L):
    s = sum(L)
    for i in range(len(L)):
        L[i] /= s
    c = [L[0] / 2]
    for i in range(1, len(L)):
        c.append(c[i - 1] + (L[i] + L[i - 1]) / 2)
    return c

def threeIllnesses(x, centers, labels):
    probabilities = []
    for i in range(40):
        p = 0.5 + 0.5 * max(0, 1 - abs(centers[i] - x) / labels[i])
        probabilities.append((p, i + 1))
    probabilities.sort(key=lambda a : (a[0], -abs(centers[a[1] - 1] - x)), reverse=True)
    return " ".join([" ".join(map(str, probabilities[i])) for i in range(3)]).replace('[','').replace(']','')

def find_probabilities(prediction):
  labels = list(map(float, "0.006 0.0056 0.003 0.2 1 0.0045 1.75 2.5 15 2 2 3 2 3 0.02 0.35 4 0.07 0.0015 3 6.7 10 3 0.2 0.03 1 1 0.8 1.2 5 10 0.1 2 5.3 0.006 0.0009 0.0072 0.0018 2 0.4".split()))
  centers = find(labels)
  return threeIllnesses(prediction, centers, labels)



token="ac31d6ff96366bb055e83e3514c3024c3b8df54794fa0f86d662d8fe887df2ea84acadffc24f13bbe5399"
api = vk_api.VkApi(token = token)
give = api.get_api()
longpoll = VkLongPoll(api)

def send_data(id, text):
    api.method('messages.send', {'user_id' : id, 'message' : text, 'random_id': 0})

for event in longpoll.listen():
    if event.type == VkEventType.MESSAGE_NEW:
        if event.from_me:
            message = event.text.lower()
            id = event.user_id
                
            if len(message.split()) == 226:
                file = open("/content/input.csv", 'w')
                print(*list(range(1, 227)), file=file, sep=';')
                res = message.split()
                for el in range(len(res)):
                  if res[el]=='0,5':
                    res[el] = '0.5'
                  if int(res[el]) < 0:
                    res[el] = '0'
                print(*res, file=file, sep=';')
                file.close()
                input = pd.read_csv('/content/input.csv', delimiter = ';')
                print (input)
                print (X_train)
                send_data(304159800, find_probabilities(model.predict(input)))
