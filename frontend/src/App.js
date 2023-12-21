import {
    createBrowserRouter,
    RouterProvider,
  } from "react-router-dom";
  
import './App.css';
import { LandingPage } from "./LandingPage.js"
import { FormPage } from "./FormPage.js"
import { Error404 } from "./Error404.js"
import { Layout } from "./Layout.js"
import { CouriersListPage } from "./CouriersListPage.js";
import { ContactInformationPage } from "./ContactInformationPage.js";
import { RegisterPage } from "./RegisterPage.js"
import { LoginPage } from "./LoginPage.js";
import { CourierPanel } from "./CourierPanel.js"
import { OfficeWorkerPanel } from "./OfficeWorkerPanel.js";
import { useState } from "react";
import { useEffect } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import { CourierLandingPage } from "./CourierLandingPage.js"
import { OfficeWorkerLandingPage } from "./OfficeWorkerLandingPage.js";
//import { ProfilePage } from "./ProfilePage.js";

  function App() {

    const { isAuthenticated, getIdTokenClaims } = useAuth0();
    const [role, setRole] = useState();

    useEffect(() => {
        const getRolesFromToken = async () => {
          try {
            if (isAuthenticated) {
              const accessToken = await getIdTokenClaims();

              setRole(accessToken["role"][0]);
            }
          } catch (error) {
            console.error('Error while decoding token:', error);
          }
        };
    
        getRolesFromToken();
      }, [getIdTokenClaims, isAuthenticated]);
    console.log(role);

    const renderLandingPage = () => {
      switch (role) {
        case "Office worker":
          return <OfficeWorkerLandingPage />;
        case "Courier":
          return <CourierLandingPage />;

        default:
          return <LandingPage />;
      }
    };


    const router = createBrowserRouter([
      {
        path: "/",
        element: <Layout/>,
        children: [
          {
            index: true,
            element: renderLandingPage(),
          },
          {
            path: "/form",
            element: <FormPage />,
          },
          {
            path: "/couriersList",
            element: <CouriersListPage />,
          },
          {
            path: "/contactInformation",
            element: <ContactInformationPage />,
          },
          {
            path: "/login",
            element: <LoginPage />,
          },
          {
            path: "/register",
            element: <RegisterPage />,
          },
          {
            path: "/courierPanel",
            element: <CourierPanel />,
          },
          {
            path: "/officeWorkerPanel",
            element: <OfficeWorkerPanel />,
          },
          {
            path: "/profile",
            element: <RegisterPage />,
          },
          {
            path: "*",
            element: <Error404 />,
          },
        ],
      },
    ]);
  
    return (
      <RouterProvider router={router} />
    );
  }
  
  export default App;
  