import {
    createBrowserRouter,
    RouterProvider,
  } from "react-router-dom";
  
import './App.css';
import {LandingPage} from "./LandingPage.js"
import {FormPage} from "./FormPage.js"
import {Error404} from "./Error404.js"
import {Layout} from "./Layout.js"
import {CouriersListPage} from "./CouriersListPage.js";
import { ContactInformationPage } from "./ContactInformationPage.js";
import {RegisterPage} from "./RegisterPage.js"
import { LoginPage } from "./LoginPage.js";
  //import {ResponsiveAppBar} from "./tmp.js"

  function App() {


    // fetch('https://localhost:7160/api/offers').then(response => response.json()).then(data => {
    //   console.log('GET response:', data);
    // })
    // .catch(error => {
    //   console.error('GET error:', error);
    // });


    const router = createBrowserRouter([
      {
        path: "/",
        element: <Layout/>,
        children: [
          {
            index: true,
            element: <LandingPage/>,
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
  