# Chat Application

This repository contains a chat application designed with the MVVM (Model-View-ViewModel) pattern. The application incorporates custom styles, animations, and various user-focused features to provide an intuitive and visually appealing chat experience.

## Requirements

### 1. MVVM Pattern
- The application strictly follows the MVVM design pattern.
- Implements clear separation between Model, View, and ViewModel components.
- Utilizes data binding for updating the UI and commands for user interactions.

### 2. Custom Styles
- All UI elements are styled using a separate, includable `ResourceDictionary`.
- Ensures consistency and ease of maintenance for the application's visual appearance.

## Features

### 1. Message Input and Display
- Users can enter messages in a designated input field.
- Messages are displayed in a scrollable chat window.
- Users can send messages by clicking the send icon or pressing the Enter key.

### 2. List of Chats
- Displays a list of users available for chat in a clear and organized manner.
- Users can easily start a new chat with a specific person.
- After sending a message, users receive a random text response, simulating a basic chatbot interaction.

### 3. Switch Between User Chats
- Users can seamlessly switch between different chat conversations.

### 4. Unread Messages
- Visual indicators for unread messages are displayed in the chat list.
- Users can quickly identify which chats have new messages without opening them.

### 5. Time and Avatar
- For every message sent, users receive a response with the following structure:
  - **Subject**: Time the message was sent, the sender's icon, and the sender's name.
  - **Body**: Text message.


## How to Run

1. Clone the repository.
2. Open the solution in your preferred development environment.
3. Build the project to restore dependencies.
4. Run the application.
