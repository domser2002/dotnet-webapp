import { useAuth0 } from "@auth0/auth0-react";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { Box, Paper, Typography } from "@mui/material";

import './App.css';
import './ProfilePage.css';

export const ProfilePage = () =>
{
    const { isAuthenticated, getIdTokenClaims } = useAuth0();
    const [profile, setProfile] = useState();
    const navigate = useNavigate();

    const [role, setRole] = useState();

    useEffect(() => {
        const getRolesFromToken = async () => {
          try {
            if (isAuthenticated) {
              const claims = await getIdTokenClaims();
    
              setRole(claims["role"][0]);
            }
          } catch (error) {
            console.error('Error while decoding token:', error);
          }
        };
    
        const fetchUserData = async () => {
          try {
            if (isAuthenticated) {
                const claims = await getIdTokenClaims();
                const id = claims["sub"].split('|')[1]
                const response = await fetch(`https://localhost:7160/api/users/subs/${id}`);

              if (response.ok) {
                const data = await response.json();
                setProfile(data); // Assuming user data is in the response
              } else {
                console.error('Error while fetching user data:', response.statusText);
              }
            }
          } catch (error) {
            console.error('Error while fetching user data:', error);
          }
        };
    
        getRolesFromToken();
        fetchUserData();
      }, [getIdTokenClaims, isAuthenticated, setProfile]);

      if(isAuthenticated)
      {
        if(role === "User")
        {
            return(
                <div className="Profile-header">
                    <Box mt={4} ml={4} p={3} component={Paper} elevation={3} sx={{ backgroundColor: '#e3f2fd' }}>
                    <Typography variant="h4" gutterBottom>
                        Witaj, {profile?.fullName || 'Użytkowniku'}
                    </Typography>
                    <Typography variant="body1" gutterBottom>
                        Email: {profile?.email}
                    </Typography>
                    <Typography variant="body1" gutterBottom>
                        Company name: {profile?.companyName}
                    </Typography>

                    {/* <Typography variant="body1" gutterBottom>
                        Company name: {profile["Address"]["Street"]}
                    </Typography> */}
                    </Box>
                </div>
            );
        }
        if(role === "Courier")
        {
            return(
                <div className="App-header-courier">

                </div>
            );
        }
        if(role === "Office worker")
        {
            return(
                <div className="App-header-officeWorker">

                </div>
            );
        }
      }
      return (
        <div>
            <label>Hello {profile} </label>
        </div>);
}