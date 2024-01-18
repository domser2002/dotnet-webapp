import './App.css';
import React, { useState } from 'react';
import { Grid, FormControl, TextField, Button, FormLabel, Box,Typography } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { useStore } from './store';
export function ContactInformationPage() {
    const navigate = useNavigate();

    const [onErrorMessage, ] = useState("");

    const [SourceStreet, setSourceStreetS] = useState("");
    const [SourceStreetNumber, setSourceStreetNumberS] = useState("");
    const [SourceFlatNumber, setSourceFlatNumberS] = useState("");
    const [SourcePostalCode, setSourcePostalCodeS] = useState("");
    const [SourceCity, setSourceCityS] = useState("");

    const [PersonalData, setPersonalDataS] = useState("");
    const [Email, setEmailS] = useState("");

    const {
        setPersonaData,
        setEmail,
        setOwnerSourceStreet,
        setOwnerSourceStreetNumber,
        setOwnerSourceFlatNumber,
        setOwnerSourcePostalCode,
        setOwnerSourceCity,
      } = useStore();

    const handleSubmit = async () => {
        setPersonaData(PersonalData);
        setEmail(Email);
        setOwnerSourceStreet(SourceStreet);
        setOwnerSourceStreetNumber(SourceStreetNumber);
        setOwnerSourceFlatNumber(SourceFlatNumber);
        setOwnerSourcePostalCode(SourcePostalCode);
        setOwnerSourceCity(SourceCity);
        navigate('/summaryPage');
    }

//     const handleSubmit = async () => {
//       try {
//           const response = await fetch('https://localhost:7160/api/ContactInformation', {
//               method: 'POST',
//               headers: {
//                   'Content-Type': 'application/json',
//               },
//               body: JSON.stringify({
//                 PersonalData: PersonalData,
//                 Email: Email,
//                 Address: {
//                   Street: SourceStreet, 
//                   StreetNumber: SourceStreetNumber, 
//                   FlatNumber: SourceFlatNumber, 
//                   PostalCode: SourcePostalCode, 
//                   City: SourceCity}}),
//           });
//           const responseData = await response.text();
//           if (response.status === 200) {
//               console.log('Pomyślnie wysłano żądanie POST do API');
//               navigate('/summaryPage');
//           } else {
//             setOnErrorMessage(`Provided data is invalid ${responseData}`);
//           }
//       } catch (error) {
//           console.error('Błąd:', error);
//       }
//   }



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
                            onChange={(e)=>setPersonalDataS(e.target.value)}
                            />
                            <TextField
                            label="E-mail"
                            variant="outlined"
                            margin="normal"
                            fullWidth
                            required
                            value={Email}
                            onChange={(e)=>setEmailS(e.target.value)}
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
                            onChange={(e)=>setSourceStreetS(e.target.value)}
                            />
                            <TextField
                            label="Street Number"
                            variant="outlined"
                            margin="normal"
                            fullWidth
                            required
                            value={SourceStreetNumber}
                            onChange={(e)=>setSourceStreetNumberS(e.target.value)}
                            />
                            <TextField
                            label="Flat Number"
                            variant="outlined"
                            margin="normal"
                            fullWidth
                            required
                            value={SourceFlatNumber}
                            onChange={(e)=>setSourceFlatNumberS(e.target.value)}
                            />
                            <TextField
                            label="Postal Code"
                            variant="outlined"
                            margin="normal"
                            fullWidth
                            required
                            value={SourcePostalCode}
                            onChange={(e)=>setSourcePostalCodeS(e.target.value)}
                            />
                            <TextField
                            label="City"
                            variant="outlined"
                            margin="normal"
                            fullWidth
                            required
                            value={SourceCity}
                            onChange={(e)=>setSourceCityS(e.target.value)}
                            />
                        </FormControl>
                    </Box>
                </Grid>
            </Grid>
        
            {onErrorMessage && 
                <Box component="section" sx={{ p: 2, border: '1px solid red', borderRadius: 8, m: 3, width: '40%',
                                marginLeft: 'auto', marginRight: 'auto'}}>
                    <Typography variant="h6" color="textSecondary">
                    {onErrorMessage}
                    </Typography>
                </Box>
            }

        <Button type="button" onClick={handleSubmit} variant="contained" sx={{color: 'white', backgroundColor: 'rgb(45, 45, 45)',}}>Submit</Button>
        
          
        </FormControl>
      </form>
      </div>
    );
  }