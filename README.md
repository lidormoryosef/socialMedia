# Social Media:

## Overview
This project is a social media platform built to enable users to connect, share content, and engage with one another through posts, comments, and more.
## Features

# User Registration
 - Users can create an account by providing the following details:
    - First Name
    - Last Name
    - Username (must be unique, ensuring a distinct identity for every user)
    - Password
    - Profile Image
# User Authentication
 - Users can log in using their registered username and password to access the platform.
# Posts
 - Users can create and share posts with their followers.
 - Users can edit their posts and share updates with their followers.
 - Users can delete posts from their feed.
# Comments
 - Users can add comments to posts, engaging in meaningful discussions.
# Likes
 - Users can express appreciation by liking:
    - Posts
    - Comments
 - Users can view the total number of likes on:
    - Posts
    - Comments
# Follow Feature
 - Users can follow others to see their posts in their personal feed.
 - Users can view:
    - The list of people they are following.
    - Who is following them.
    - Who is following another specific user.
    - The people another user is following.


## How to Run 

### To run the Social Media application, follow these steps:

1. Open terminal in your local machine.

2. Running the following command: `docker pull lidormoryosef/socialmedia:001`.

3. Running the following command: `docker run -d -p 8080:8080 lidormoryosef/socialmedia:001`.

4. Once the server is running, open any web browser and navigate to [http://localhost:8080/swagger](http://localhost:8080/swagger).


## Database

Safe Driver utilizes MongoDB as its database to store users and statistics information. MongoDB is a popular NoSQL database known for its scalability and flexibility in managing data.

### Using MongoDB

To use MongoDB with Safe Driver, follow these steps:

1. Visit the [MongoDB website](https://www.mongodb.com/) and download the latest version of MongoDB suitable for your operating system.

2. Follow the installation instructions provided by MongoDB to set up and configure the database.

3. Start MongoDB by running the appropriate command for your operating system.

4. Safe Driver will automatically connect to the running MongoDB instance and use it to store and retrieve data.

## Requirements

Before running Safe Driver, ensure that you have the following requirements fulfilled:

1. yolov5: To clone YOLOv5, navigate to the AIModel directory and run the following command: `git clone https://github.com/ultralytics/yolov5.git`. 

2. MongoDB: Install MongoDB to set up the database for Safe Driver. Follow the steps mentioned in the "Using MongoDB" section above to install and configure MongoDB.

3. Expo Go: Install the Expo Go application on your phone. You can then use Expo Go to run the Safe Driver application..

You are now all set to run Safe Driver and explore the world of safe driving! Take care and drive safely!
