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
import { CourierPanel } from "./CourierPanel.js"
import { OfficeWorkerPanel } from "./OfficeWorkerPanel.js";
import { useState } from "react";
import { useEffect } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import { CourierLandingPage } from "./CourierLandingPage.js"
import { OfficeWorkerLandingPage } from "./OfficeWorkerLandingPage.js";
import { SummaryPage } from "./SummaryPage.js";
import { ProfilePage } from "./ProfilePage.js";
import { RequestDetails } from "./RequestDetails.js";
import { CourierRequestDetails } from "./CourierRequestDetails.js";
import { UserRequestDetails } from "./UserRequestDetails.js";
import { CompanyOffers } from "./CompanyOffers.js"
import { OfferDetails } from "./OfferDetails.js";

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
        case "OfficeWorker":
          return <OfficeWorkerLandingPage />;
        case "Courier":
          return <CourierLandingPage />;

        default:
          return <LandingPage />;
      }
    };

    const renderCourierPage = () => {
      switch (role) {
        case "Courier":
          return <CourierPanel />;

        default:
          return <Error404 />;
      }
    };

    const renderOfficeWorkerPage = () => {
      switch (role) {
        case "OfficeWorker":
          return <OfficeWorkerPanel />;

        default:
          return <Error404 />;
      }
    };

    const renderFormPage = () => {
        if(!isAuthenticated)
        {
          return <FormPage/>;
        }
        else
        {
          if(role === "User")
          {
            return <FormPage/>;
          }
        }
        return <Error404/>;
      }
    

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
            element: renderFormPage(),
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
            path: "/summaryPage",
            element: <SummaryPage />,
          },
          {
            path: "/courierPanel",
            element: renderCourierPage(),
          },
          {
            path: "/officeWorkerPanel",
            element: renderOfficeWorkerPage(),
          },
          {
            path: "/profile",
            element: <ProfilePage />,
          },
          {
            path: "/register",
            element: <RegisterPage />,
          },
          {
            path: "/officeWorkerPanel/requestDetails",
            element: <RequestDetails />,
          },
          {
            path: "/courierPanel/courierRequestDetails",
            element: <CourierRequestDetails />,
          },
          {
            path: "/profile/userRequestDetails",
            element: <UserRequestDetails />,
          },
          {
            path: "/companyOffers",
            element: <CompanyOffers />,
          },
          {
            path: "/companyOffers/offerDetails",
            element: <OfferDetails />,
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
  