# CourierHub
## What is CourierHub?
CourierHub is an application created for Courier companies and its users. User can log in using any account (Google, Microsoft, etc.) and order a package delivery. Courier can change status of any request for his company. Office worker can manage offers. This app takes care of all details of package delivery process and supports connection with multiple companies. Project specification is placed in Project.md file
## Technologies used
Backend application is written in ASP .NET Framework 7.0. Database is stored on Azure. Authorization is performed by OAuth 2.0 by using Auth0 service. Frontend application is written in React.js. Frontend is using MUI component and styles library.
## Testing
Project contains a large suit of unit and integration tests covering most of features (and all key features). UI tests cover three features and are written in Jest. Also, there is one E2E test written using Cypress technology. That test covers main feature of our application which is creating inquiry, choosing offer and submitting it
## Deployment
Frontend application is hosted on Azure service separately from backend. Frontend is available on: https://courierhubreact.azurewebsites.net/
## How to run?
To use application visit the site: https://courierhubreact.azurewebsites.net/
To open this project on your local machine, firstly clone our repository and switch directory to ./frontend. Node.js must be installed. Simply use these commands:
```bash
npm install
npm start
```
After these you are able to edit frontend part of our project and see effects without closing app.

