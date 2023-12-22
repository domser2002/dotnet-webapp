import './App.css';
import React, { useState } from 'react';
import { Grid, FormControl, TextField, Button, FormLabel, Box } from '@mui/material';
import { useNavigate } from 'react-router-dom';

import IconButton from '@mui/material/IconButton';
import InputAdornment from '@mui/material/InputAdornment';
import Visibility from '@mui/icons-material/Visibility';
import VisibilityOff from '@mui/icons-material/VisibilityOff';


export function RegisterPage() {
    const navigate = useNavigate();

    const handleSubmit = async () => {
      try {
          const response = await fetch('https://localhost:7160/api/users', {
              method: 'POST',
              headers: {
                  'Content-Type': 'application/json',
              },
              body: JSON.stringify({
                FirstName: FirstName,
                LastName: LastName,
                Company: Company,
                Email: Email,
                SourceAddress: {
                  Street: SourceStreet, 
                  StreetNumber: SourceStreetNumber, 
                  FlatNumber: SourceFlatNumber, 
                  PostalCode: SourcePostalCode, 
                  City: SourceCity},
                DefaultSourceAddress: {
                    DefaultStreet: DefaultSourceStreet, 
                    DefaultStreetNumber: DefaultSourceStreetNumber, 
                    DefaultFlatNumber: DefaultSourceFlatNumber, 
                    DefaultPostalCode: DefaultSourcePostalCode, 
                    DefaultCity: DefaultSourceCity}}),
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
  const handleTogglePasswordVisibility = () => {
    setShowPassword(!showPassword);
  };
    const [showPassword, setShowPassword] = useState(false);
    const [FirstName, setFisrtName] = useState("");
    const [LastName, setLastName] = useState("");
    const [Email, setEmail] = useState("");
    const [Company, setCompany] = useState("");
    const [Password, setPassword] = useState("");

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
                            <TextField
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

            <Button type="submit" variant="contained" sx={{color: 'white', backgroundColor: 'rgb(45, 45, 45)',}}>Register</Button>
          
        </FormControl>
      </form>
      </div>
    );
  }