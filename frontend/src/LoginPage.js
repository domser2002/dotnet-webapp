import './App.css';
import React, { useState } from 'react';
import { Grid, FormControl, TextField, Button, FormLabel, Box } from '@mui/material';
//import { useNavigate } from 'react-router-dom';
import { LoginButton } from './LoginButton';

export function LoginPage() {
    //const navigate = useNavigate();

    const handleSubmit = async () => {
    //   try {
    //       const response = await fetch('https://localhost:7160/api/users', {
    //           method: 'POST',
    //           headers: {
    //               'Content-Type': 'application/json',
    //           },
    //           body: JSON.stringify({
    //             FirstName: FirstName,
    //             LastName: LastName,
    //             Company: Company,
    //             Email: Email,
    //             SourceAddress: {
    //               Street: SourceStreet, 
    //               StreetNumber: SourceStreetNumber, 
    //               FlatNumber: SourceFlatNumber, 
    //               PostalCode: SourcePostalCode, 
    //               City: SourceCity},
    //             DefaultSourceAddress: {
    //                 DefaultStreet: DefaultSourceStreet, 
    //                 DefaultStreetNumber: DefaultSourceStreetNumber, 
    //                 DefaultFlatNumber: DefaultSourceFlatNumber, 
    //                 DefaultPostalCode: DefaultSourcePostalCode, 
    //                 DefaultCity: DefaultSourceCity}}),
    //       });

    //       if (response.ok) {
    //           console.log('Pomyślnie wysłano żądanie POST do API');
    //           navigate('/landingPage');
    //       } else {
    //           console.error('Błąd podczas wysyłania żądania POST do API');
    //       }
    //   } catch (error) {
    //       console.error('Błąd:', error);
    //   }
  }

    const [Email, setEmail] = useState("");
    const [Password, setPassword] = useState("");

    return (
      <div className="App-header">
        <form onSubmit={handleSubmit}>
        <FormControl fullWidth>
            <Grid container spacing={2}>
                <Grid item>
                    <Box component="section" sx={{ p: 2, border: '1px solid grey', borderRadius: 8, m: 3}}>
                        <FormControl variant='outlined'>
                            <FormLabel >Login</FormLabel>
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
                            fullWidth
                            required
                            value={Password}
                            onChange={(e)=>setPassword(e.target.value)}
                            />
                        </FormControl>
                    </Box>
                </Grid>
            </Grid>

            <Button type="submit" variant="contained" sx={{color: 'white', backgroundColor: 'rgb(45, 45, 45)',}}>Login</Button>

        </FormControl>

      </form>
      <LoginButton/>
      </div>
    );
  }