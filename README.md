# Social Media:

## Overview
This project is a social media platform built to enable users to connect, share content, and engage with one another through posts, comments, and more.
# Features

## User Registration
 - Users can create an account by providing the following details:
    - First Name
    - Last Name
    - Username (must be unique, ensuring a distinct identity for every user)
    - Password
    - Profile Image
## User Authentication
 - Users can log in using their registered username and password to access the platform.
## Posts
 - Users can create and share posts with their followers.
 - Users can edit their posts and share updates with their followers.
 - Users can delete posts from their feed.
## Comments
 - Users can add comments to posts, engaging in meaningful discussions.
 - Users can edit their comments to correct or update information.
 - Users can delete their comments when necessary.
## Likes
 - Users can express appreciation by liking:
    - Posts
    - Comments
 - Users can view the total number of likes on:
    - Posts
    - Comments
## Follow Feature
 - Users can follow others to see their posts in their personal feed.
 - Users can view:
    - The list of people they are following.
    - Who is following them.
    - Who is following another specific user.
    - The people another user is following.


# How to Run 

## To run the Social Media application, follow these steps:

1. Open terminal in your local machine.

2. Running the following command: `docker pull lidormoryosef/socialmedia:001`.

3. Running the following command: `docker run -d -p 8080:8080 lidormoryosef/socialmedia:001`.

4. Once the server is running, open any web browser and navigate to [http://localhost:8080/swagger](http://localhost:8080/swagger).


# Database
The Social Media Platform utilizes SQL Server as its database to store user information, posts, comments, likes, and follower relationships. SQL Server is a relational database management system known for its reliability, scalability, and advanced data-handling capabilities.

# Requirements

Before running Social Media, ensure that you have the following requirements fulfilled:

1. Docker. 

