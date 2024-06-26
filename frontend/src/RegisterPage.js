import { useAuth0 } from "@auth0/auth0-react";
import React, { useState, useEffect } from 'react';
import { Grid, FormControl, TextField, Button, FormLabel, Box, Typography } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { LoadingPage } from './LoadingPage';

import './App.css';


export function RegisterPage() {

    const baseUrl = process.env.REACT_APP_API_URL;
    const apiUsersSubsId = baseUrl+"/api/users/subs";
    const apiUsers = baseUrl+"/api/users";

    const navigate = useNavigate();

    const { isAuthenticated, getIdTokenClaims } = useAuth0();
    const [profileId, setProfileId] = useState();
    const [userInDb, setUserInDb] = useState(false);
    const [onErrorMessage, setOnErrorMessage] = useState("");

    const [FirstName, setFirstName] = useState("");
    const [LastName, setLastName] = useState("");
    const [Email, setEmail] = useState("");
    const [Company, setCompany] = useState("");

    const [SourceStreet, setSourceStreet] = useState("");
    const [SourceStreetNumber, setSourceStreetNumber] = useState("");
    const [SourceFlatNumber, setSourceFlatNumber] = useState("");
    const [SourcePostalCode, setSourcePostalCode] = useState("");
    const [SourceCity, setSourceCity] = useState("");

    const [DefaultSourceStreet, setDefaultSourceStreet] = useState("");
    const [DefaultSourceStreetNumber, setDefaultSourceStreetNumber] = useState("");
    const [DefaultSourceFlatNumber, setDefaultSourceFlatNumber] = useState("");
    const [DefaultSourcePostalCode, setDefaultSourcePostalCode] = useState("");
    const [DefaultSourceCity, setDefaultSourceCity] = useState("");

    // const [hiddenFirstName, setHiddenFirstName] = useState(false);
    // const [hiddenLastName, setHiddenLastName] = useState(false);
    // const [hiddenEmail, setHiddenEmail] = useState(false);

    const [hiddenFirstName] = useState(false);
    const [hiddenLastName] = useState(false);
    const [hiddenEmail] = useState(false);

    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
      setIsLoading(true);
        const getIdFromToken = async () => {
          try {
            if (isAuthenticated) {
              const claims = await getIdTokenClaims();

              setProfileId(claims["sub"].split('|')[1]);

              const resp = await getUserById(claims["sub"].split('|')[1]);
              if (resp) {
                setUserInDb(true);
                
              } else {
                setFirstName(claims["given_name"]);
                setLastName(claims["family_name"]);
                setEmail(claims["email"]);

                // setHiddenFirstName(true);
                // setHiddenLastName(true);
                // setHiddenEmail(true);
              }
            }
            setIsLoading(false);
          } catch (error) {
            console.error('Error while decoding token:', error);
            setIsLoading(false);
          }
        };
    
        const getUserById = async (id) => {
          try {
            const claims = await getIdTokenClaims();
            const response = await fetch(`${apiUsersSubsId}/${id}`, {
              headers: {
                  Authorization: `Bearer ${claims["__raw"]}`,
              },
          });
            if(response.status === 200)
            {
              return true;
            }
            if(response.status === 404)
            {
              return false;
            }
          } catch (error) {
            return false;
          }
        };

        getIdFromToken();
      }, [getIdTokenClaims, isAuthenticated, navigate, apiUsersSubsId]);


    const handleSubmit = async () => {
      try {
        const claims = await getIdTokenClaims();
          const response = await fetch(apiUsers, {
              method: 'POST',
              headers: {
                  'Content-Type': 'application/json',
                  Authorization: `Bearer ${claims["__raw"]}`,
              },
              body: JSON.stringify({
                FirstName: FirstName,
                LastName: LastName,
                CompanyName: Company,
                Email: Email,
                Auth0Id: profileId,
                Address: {
                  Street: SourceStreet, 
                  StreetNumber: SourceStreetNumber, 
                  FlatNumber: SourceFlatNumber, 
                  PostalCode: SourcePostalCode, 
                  City: SourceCity},
                DefaultSourceAddress: {
                    Street: DefaultSourceStreet, 
                    StreetNumber: DefaultSourceStreetNumber, 
                    FlatNumber: DefaultSourceFlatNumber, 
                    PostalCode: DefaultSourcePostalCode, 
                    City: DefaultSourceCity}}),
          });

          const responseData = await response.text();

          const responseStatus = response.status;

          if (responseStatus === 201) {
              console.log('Pomyślnie wysłano żądanie POST do API');
              navigate('/');
          }
          else {
            console.log(JSON.stringify(responseData));
            setOnErrorMessage(`Provided data is invalid ${responseData}`);
          }
      } catch (error) {
          console.error('Błąd:', error);
      }
  }

    if(userInDb)
    {
      navigate("./profile");
    }

    if(isLoading)
    {
      return(
        <LoadingPage/>
      );
    }

    return (
      <div className="App-header">
        <form>
        <FormControl fullWidth>
            <Grid container spacing={2}>
                <Grid item>
                    <Box component="section" sx={{ p: 2, border: '1px solid grey', borderRadius: 8, m: 3}}>
                        <FormControl variant='outlined'>
                            <FormLabel >Personal data</FormLabel>
                            <TextField
                            label="First name"
                            variant="outlined"
                            margin="normal"
                            fullWidth
                            required
                            value={FirstName}
                            disabled={hiddenFirstName}
                            onChange={(e)=>setFirstName(e.target.value)}
                            />
                            <TextField
                            label="Last name"
                            variant="outlined"
                            margin="normal"
                            fullWidth
                            required
                            value={LastName}
                            disabled={hiddenLastName}
                            onChange={(e)=>setLastName(e.target.value)}
                            />
                            <TextField
                            label="Company Name"
                            variant="outlined"
                            margin="normal"
                            fullWidth
                            required
                            value={Company}
                            onChange={(e)=>setCompany(e.target.value)}
                            />
                            <TextField
                            label="E-mail"
                            variant="outlined"
                            margin="normal"
                            fullWidth
                            required
                            value={Email}
                            disabled={hiddenEmail}
                            onChange={(e)=>setEmail(e.target.value)}
                            />
                        </FormControl>
                    </Box>
                </Grid>
                <Grid item>
                    <Box component="section" sx={{ p: 2, border: '1px solid grey', borderRadius: 8, m: 3}}>
                        <FormControl variant='outlined'>
                            <FormLabel>Address</FormLabel>
                            <TextField
                            label="Street"
                            variant="outlined"
                            margin="normal"
                            fullWidth
                            required
                            value={SourceStreet}
                            onChange={(e)=>setSourceStreet(e.target.value)}
                            />
                            <TextField
                            label="Street Number"
                            variant="outlined"
                            margin="normal"
                            fullWidth
                            required
                            value={SourceStreetNumber}
                            onChange={(e)=>setSourceStreetNumber(e.target.value)}
                            />
                            <TextField
                            label="Flat Number"
                            variant="outlined"
                            margin="normal"
                            fullWidth
                            required
                            value={SourceFlatNumber}
                            onChange={(e)=>setSourceFlatNumber(e.target.value)}
                            />
                            <TextField
                            label="Postal Code"
                            variant="outlined"
                            margin="normal"
                            fullWidth
                            required
                            value={SourcePostalCode}
                            onChange={(e)=>setSourcePostalCode(e.target.value)}
                            />
                            <TextField
                            label="City"
                            variant="outlined"
                            margin="normal"
                            fullWidth
                            required
                            value={SourceCity}
                            onChange={(e)=>setSourceCity(e.target.value)}
                            />
                        </FormControl>
                    </Box>
                </Grid>
                <Grid item>
                    <Box component="section" sx={{ p: 2, border: '1px solid grey', borderRadius: 8, m: 3}}>
                        <FormControl variant='outlined'>
                            <FormLabel>Default source address</FormLabel>
                            <TextField
                            label="Street"
                            variant="outlined"
                            margin="normal"
                            fullWidth
                            required
                            value={DefaultSourceStreet}
                            onChange={(e)=>setDefaultSourceStreet(e.target.value)}
                            />
                            <TextField
                            label="Street Number"
                            variant="outlined"
                            margin="normal"
                            fullWidth
                            required
                            value={DefaultSourceStreetNumber}
                            onChange={(e)=>setDefaultSourceStreetNumber(e.target.value)}
                            />
                            <TextField
                            label="Flat Number"
                            variant="outlined"
                            margin="normal"
                            fullWidth
                            required
                            value={DefaultSourceFlatNumber}
                            onChange={(e)=>setDefaultSourceFlatNumber(e.target.value)}
                            />
                            <TextField
                            label="Postal Code"
                            variant="outlined"
                            margin="normal"
                            fullWidth
                            required
                            value={DefaultSourcePostalCode}
                            onChange={(e)=>setDefaultSourcePostalCode(e.target.value)}
                            />
                            <TextField
                            label="City"
                            variant="outlined"
                            margin="normal"
                            fullWidth
                            required
                            value={DefaultSourceCity}
                            onChange={(e)=>setDefaultSourceCity(e.target.value)}
                            />
                        </FormControl>
                    </Box>
                </Grid>
            </Grid>


            {onErrorMessage &&           
              <Box component="section" sx={{ p: 2, border: '1px solid red', borderRadius: 8, m: 3, width: '40%', marginLeft: 'auto', marginRight: 'auto'}}>
                <Typography variant="h6" color="textSecondary">{onErrorMessage}</Typography>
              </Box>}

            <Button type="button" onClick={handleSubmit} variant="contained" sx={{color: 'white', backgroundColor: 'rgb(45, 45, 45)',}}>Register</Button>
          
        </FormControl>
      </form>
      </div>
    );
  }