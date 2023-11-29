import './App.css';
import React, { useState } from 'react';
import { Grid, FormControl, TextField, Button, FormLabel, Box } from '@mui/material';
import { useNavigate } from 'react-router-dom';

export function ContactInformationPage() {
    const navigate = useNavigate();


    const [SourceStreet, setSourceStreet] = useState("");
    const [SourceStreetNumber, setSourceStreetNumber] = useState("");
    const [SourceFlatNumber, setSourceFlatNumber] = useState("");
    const [SourcePostalCode, setSourcePostalCode] = useState("");
    const [SourceCity, setSourceCity] = useState("");

    const [PersonalData, setPersonalData] = useState("");
    const [Email, setEmail] = useState("");


    const handleSubmit = async () => {
      try {
          const response = await fetch('https://localhost:7160/api/contacts', {
              method: 'POST',
              headers: {
                  'Content-Type': 'application/json',
              },
              body: JSON.stringify({
                PersonalData: PersonalData,
                Email: Email,
                Address: {
                  Street: SourceStreet, 
                  StreetNumber: SourceStreetNumber, 
                  FlatNumber: SourceFlatNumber, 
                  PostalCode: SourcePostalCode, 
                  City: SourceCity}}),
          });

          if (response.ok) {
              console.log('Pomyślnie wysłano żądanie POST do API');
              navigate('/couriersList');
          } else {
              console.error('Błąd podczas wysyłania żądania POST do API');
          }
      } catch (error) {
          console.error('Błąd:', error);
      }
  }



    return (
      <div className="App-header">
        <form onSubmit={handleSubmit}>
        <FormControl fullWidth>
            <Grid container spacing={2}>
            <Grid item>
                    <Box component="section" sx={{ p: 2, border: '1px solid grey', borderRadius: 8, m: 3}}>
                        <FormControl variant='outlined'>
                            <FormLabel>Personal data</FormLabel>
                            <TextField
                            label="Personal data"
                            variant="outlined"
                            margin="normal"
                            fullWidth
                            required
                            value={PersonalData}
                            onChange={(e)=>setPersonalData(e.target.value)}
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
                        </FormControl>
                    </Box>
                </Grid>
                <Grid item>
                    <Box component="section" sx={{ p: 2, border: '1px solid grey', borderRadius: 8, m: 3}}>
                        <FormControl variant='outlined'>
                            <FormLabel>Source Address</FormLabel>
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
            </Grid>
        

        <Button type="submit" variant="contained" sx={{color: 'white', backgroundColor: 'rgb(45, 45, 45)',}}>Submit</Button>
        
          
        </FormControl>
      </form>
      </div>
    );
  }