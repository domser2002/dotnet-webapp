import './App.css';
import { useAuth0 } from "@auth0/auth0-react";
import React, { useState, useEffect } from 'react';
import { Grid, FormControl, TextField, Button, FormLabel, Box, Typography } from '@mui/material';
import { useNavigate } from 'react-router-dom';


export function RegisterPage() {
    const navigate = useNavigate();

    const { isAuthenticated, getIdTokenClaims } = useAuth0();
    const [profileId, setProfileId] = useState();
    const [userInDb, setUserInDb] = useState(true);
    //const [user, setUser] = useState();
    useEffect(() => {
        const getIdFromToken = async () => {
          try {
            if (isAuthenticated) {
              const accessToken = await getIdTokenClaims();
    
              const newProfileId = accessToken["sub"].split('|')[1].slice(-2);
              setProfileId(newProfileId);
    
              // Przenieś resztę logiki do bloku then
              const resp = await getUserById(25);
              if (resp) {
                // Zrób coś, jeśli użytkownik istnieje
              } else {
                //await handleSaveUser(newProfileId);
              }
            }
          } catch (error) {
            console.error('Error while decoding token:', error);
          }
        };
    
        const getUserById = async (id) => {
          try {
            const response = await fetch(`https://localhost:7160/api/users/${id}`);
            if(response.status === 200)
            {
              setUserInDb(true);
              return true;
            }

          } catch (error) {
            console.error('Error fetching user:', error);
            return false;
          }
        };
    
        // const handleSaveUser = async (id) => {
        //   try {
        //     const response = await fetch(`https://localhost:7160/api/users`, {
        //       method: 'POST',
        //       headers: {
        //         'Content-Type': 'application/json',
        //       },
        //       body: JSON.stringify({
        //         Id: id,
        //         FirstName: "aaaa",
        //         LastName: "aaaa",
        //         CompanyName: "cccccc",
        //         Email: "mdp.ch3@gmail.com",
        //         Address: {
        //             Street: "SourceStreet", 
        //             StreetNumber: "0", 
        //             FlatNumber: "0", 
        //             PostalCode: "26-706", 
        //             City: "SourceCity"},
        //           DefaultSourceAddress: {
        //             Street: "SourceStreet", 
        //             StreetNumber: "0", 
        //             FlatNumber: "0", 
        //             PostalCode: "26-706", 
        //             City: "SourceCity"},
        //       }),
        //     });
    
        //     if (response.ok) {
        //       console.log('User created successfully');
        //       // Tutaj możesz dodać dodatkową logikę po zapisaniu użytkownika
        //     } else {
        //       console.error('Failed to create user:', response.statusText);
        //     }
        //   } catch (error) {
        //     console.error('Error while creating user:', error);
        //   }
        // };
    
        getIdFromToken();
    
      }, [getIdTokenClaims, isAuthenticated]);


    const handleSubmit = async () => {
      try {
          const response = await fetch('https://localhost:7160/api/users', {
              method: 'POST',
              headers: {
                  'Content-Type': 'application/json',
              },
              body: JSON.stringify({
                Id: profileId,
                FirstName: FirstName,
                LastName: LastName,
                CompanyName: Company,
                Email: Email,
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

          if (response.ok) {
              console.log('Pomyślnie wysłano żądanie POST do API');
              navigate('/landingPage');
          } else {
              console.error('Błąd podczas wysyłania żądania POST do API');
          }
      } catch (error) {
          console.error('Błąd:', error);
      }
  }
  // const handleTogglePasswordVisibility = () => {
  //   setShowPassword(!showPassword);
  // };
    //const [showPassword, setShowPassword] = useState(false);
    const [FirstName, setFisrtName] = useState("");
    const [LastName, setLastName] = useState("");
    const [Email, setEmail] = useState("");
    const [Company, setCompany] = useState("");
    //const [Password, setPassword] = useState("");

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

    if(userInDb)
    {
      return (
        <div className="App-header">
          <Typography variant="h2" color="primary" gutterBottom>
            Hello user of id:{profileId}
          </Typography>
        </div>
      );
    }

    return (
      <div className="App-header">
        <form onSubmit={handleSubmit}>
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
                            onChange={(e)=>setFisrtName(e.target.value)}
                            />
                            <TextField
                            label="Last name"
                            variant="outlined"
                            margin="normal"
                            fullWidth
                            required
                            value={LastName}
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
                            onChange={(e)=>setEmail(e.target.value)}
                            />
                            {/* <TextField
                            label="Password"
                            variant="outlined"
                            margin="normal"
                            type={showPassword ? 'text' : 'password'}
                            fullWidth
                            required
                            value={Password}
                            onChange={(e)=>setPassword(e.target.value)}

                            InputProps={{
                                endAdornment: (
                                  <InputAdornment position="end">
                                    <IconButton onClick={handleTogglePasswordVisibility}>
                                      {showPassword ? <Visibility /> : <VisibilityOff />}
                                    </IconButton>
                                  </InputAdornment>
                                ),
                              }}
                            /> */}
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

            <Button type="submit" variant="contained" sx={{color: 'white', backgroundColor: 'rgb(45, 45, 45)',}}>Register</Button>
          
        </FormControl>
      </form>
      </div>
    );
  }