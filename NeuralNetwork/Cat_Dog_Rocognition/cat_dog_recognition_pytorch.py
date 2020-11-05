import torch
from torch import nn
from torch import optim
import torch.nn.functional as F
from torchvision import datasets, transforms
import matplotlib.pyplot as plt
import helper

#---
# GET DATA
#---
"""
1. Download data from https://www.kaggle.com/c/dogs-vs-cats
2. Create Cat_Dog_data
3. For cats create folder cat in Cat_Dog_data folder
4. For dogs create folder dog in Cat_Dog_data folder
"""

data_dir = 'Cat_Dog_data'

# Define transforms for the training data and testing data
train_transforms = transforms.Compose([transforms.Resize(255),
                                      transforms.CenterCrop(224),
                                      transforms.ToTensor()])

# Pass transforms in here, then run the next cell to see how the transforms look
train_data = datasets.ImageFolder(data_dir + '/train', transform=train_transforms)
# test_data = datasets.ImageFolder(data_dir + '/test', transform=test_transforms)

trainloader = torch.utils.data.DataLoader(train_data, batch_size=32)
# testloader = torch.utils.data.DataLoader(test_data, batch_size=32)

# Checking shape of image
#dataiter = iter(trainloader)
#images, labels =dataiter.next()
#print(type(images))
#print(images.shape)
#print(labels.shape)

#---
# NEURAL NETWORK
#---
model = nn.Sequential(nn.Linear(150528, 50176),
                      nn.ReLU(),
                      nn.Linear(50176, 12544),
                      nn.ReLU(),  
                      nn.Linear(12544, 3136), 
                      nn.ReLU(),  
                      nn.Linear(3136, 32), 
                      nn.ReLU(),  
                      nn.Linear(32, 2),
                      nn.LogSoftmax(dim=1)) 
    
#---
# INIT MODEL
#---
criterion = nn.NLLLoss()
optimizer = optim.SGD(model.parameters(), lr=0.003)

#---
# MODEL LEARNIG PROCESS
#---
epochs = 5

for e in range(epochs):
	running_loss = 0
	for images, labels in trainloader:
	        images = images.view(images.shape[0], -1)
    
	        optimizer.zero_grad()
	        output = model(images)
	        loss = criterion(output, labels)  
	        loss.backward()
	        optimizer.step()
	        running_loss += loss.item()
	else:
        	print(f"Training loss: {running_loss/len(trainloader)}")
            
#---
# USE MODEL
#---
# Grab some data 
dataiter = iter(trainloader)
images, labels = dataiter.next()

# Resize images into a 1D vector, new shape is (batch size, color channels, image pixels) 
images.resize_(32, 1, 2700)
# or images.resize_(images.shape[0], 1, 784) to automatically get batch size

# Forward pass through the network
img_idx = 0
ps = model.forward(images[img_idx,:])

img = images[img_idx]
helper.view_classify(img.view(3, 224, 224), ps)
