import './App.css';
import React, { useState, useEffect } from 'react';
import { Grid, Checkbox, FormControl, InputLabel, Select, MenuItem, TextField, Button, FormLabel, FormControlLabel, Box, Typography } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { DatePicker, LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { useAuth0 } from '@auth0/auth0-react';
import {useStore} from './store';
import { LoadingPage } from './LoadingPage';

export function FormPage() {
    const navigate = useNavigate();

    const SourceStreet = useStore((state) => state.SourceStreet);
    const SourceStreetNumber = useStore((state) => state.SourceStreetNumber);
    const SourceFlatNumber = useStore((state) => state.SourceFlatNumber);
    const SourcePostalCode = useStore((state) => state.SourcePostalCode);
    const SourceCity = useStore((state) => state.SourceCity);
  
    const DestinationStreet = useStore((state) => state.DestinationStreet);
    const DestinationStreetNumber = useStore((state) => state.DestinationStreetNumber);
    const DestinationFlatNumber = useStore((state) => state.DestinationFlatNumber);
    const DestinationPostalCode = useStore((state) => state.DestinationPostalCode);
    const DestinationCity = useStore((state) => state.DestinationCity);
  
    const DeliveryAtWeekend = useStore((state) => state.DeliveryAtWeekend);
  
    const Priority = useStore((state) => state.Priority);
  
    const Length = useStore((state) => state.Length);
    const Width = useStore((state) => state.Width);
    const Height = useStore((state) => state.Height);
    const Weight = useStore((state) => state.Weight);
  
    const DateFrom = useStore((state) => state.DateFrom);
    const DateTo = useStore((state) => state.DateTo);
  
    const setSourceStreet = useStore((state) => state.setSourceStreet);
    const setSourceStreetNumber = useStore((state) => state.setSourceStreetNumber);
    const setSourceFlatNumber = useStore((state) => state.setSourceFlatNumber);
    const setSourcePostalCode = useStore((state) => state.setSourcePostalCode);
    const setSourceCity = useStore((state) => state.setSourceCity);
  
    const setDestinationStreet = useStore((state) => state.setDestinationStreet);
    const setDestinationStreetNumber = useStore((state) => state.setDestinationStreetNumber);
    const setDestinationFlatNumber = useStore((state) => state.setDestinationFlatNumber);
    const setDestinationPostalCode = useStore((state) => state.setDestinationPostalCode);
    const setDestinationCity = useStore((state) => state.setDestinationCity);
  
    const setDeliveryAtWeekend = useStore((state) => state.setDeliveryAtWeekend);
  
    const setPriority = useStore((state) => state.setPriority);
  
    const setLength = useStore((state) => state.setLength);
    const setWidth = useStore((state) => state.setWidth);
    const setHeight = useStore((state) => state.setHeight);
    const setWeight = useStore((state) => state.setWeight);
  
    const setDateFrom = useStore((state) => state.setDateFrom);
    const setDateTo = useStore((state) => state.setDateTo);


    // const [SourceStreet, setSourceStreet] = useState("");
    // const [SourceStreetNumber, setSourceStreetNumber] = useState("");
    // const [SourceFlatNumber, setSourceFlatNumber] = useState("");
    // const [SourcePostalCode, setSourcePostalCode] = useState("");
    // const [SourceCity, setSourceCity] = useState("");

    // const [DestinationStreet, setDestinationStreet] = useState("");
    // const [DestinationStreetNumber, setDestinationStreetNumber] = useState("");
    // const [DestinationFlatNumber, setDestinationFlatNumber] = useState("");
    // const [DestinationPostalCode, setDestinationPostalCode] = useState("");
    // const [DestinationCity, setDestinationCity] = useState("");

    // const [DeliveryAtWeekend, setDeliveryAtWeekend] = useState(true);

    // const [Priority, setPriority] = useState(0);

    // const [Length, setLength] = useState("");
    // const [Width, setWidth] = useState("");
    // const [Height, setHeight] = useState("");
    // const [Weight, setWeight] = useState("");

    // const [DateFrom, setDateFrom] = useState(null);
    // const [DateTo, setDateTo] = useState(null);

    const [isLoading, setIsLoading] = useState(false);

    const [onErrorMessage, setOnErrorMessage] = useState("");

    const {getIdTokenClaims, isAuthenticated} = useAuth0();

    const [hiddenSourceStreet, setHiddenSourceStreet] = useState(false);
    const [hiddenSourceStreetNumber, setHiddenSourceStreetNumber] = useState(false);
    const [hiddenSourceFlatNumber, setHiddenSourceFlatNumber] = useState(false);
    const [hiddenSourcePostalCode, setHiddenSourcePostalCode] = useState(false);
    const [hiddenSourceCity, setHiddenSourceCity] = useState(false);

    const [user, setUser] = useState();

    useEffect(() => {
      setIsLoading(true);
        const getIdFromToken = async () => {
          try {
            if (isAuthenticated) {
              const claims = await getIdTokenClaims();

              const resp = await getUserById(claims["sub"].split('|')[1]);
            }
            setIsLoading(false);
          } catch (error) {
            console.error('Error while decoding token:', error);
            setIsLoading(false);
          }
        };
    
        const getUserById = async (id) => {
          try {
            const response = await fetch(`https://localhost:7160/api/users/subs/${id}`);
            if(response.status === 200)
            {
              const body = await response.json();
              setUser(body);
              setSourceStreet(body["defaultSourceAddress"]["street"]);
              setSourceStreetNumber(body["defaultSourceAddress"]["streetNumber"]);
              setSourceFlatNumber(body["defaultSourceAddress"]["flatNumber"]);
              setSourcePostalCode(body["defaultSourceAddress"]["postalCode"]);
              setSourceCity(body["defaultSourceAddress"]["city"]);
              
              setHiddenSourceStreet(true);
              setHiddenSourceStreetNumber(true);
              setHiddenSourceFlatNumber(true);
              setHiddenSourcePostalCode(true);
              setHiddenSourceCity(true);
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
      }, [getIdTokenClaims, isAuthenticated, setSourceCity, setSourceFlatNumber, setSourcePostalCode, setSourceStreet, setSourceStreetNumber]);



    const handleSubmit = async () => {
      setIsLoading(true);
      try {
          const response = await fetch('http://localhost:5261/api/inquiries', {
              method: 'POST',
              headers: {
                  'Content-Type': 'application/json',
              },
              body: JSON.stringify({
                Package: {
                  Length: Length, 
                  Width: Width, 
                  Height: Height, 
                  Weight: Weight
                },
                PickupDate: DateFrom.toISOString(),
                DeliveryDate: DateTo.toISOString(),
                SourceAddress: {
                  Street: SourceStreet, 
                  StreetNumber: SourceStreetNumber, 
                  FlatNumber: SourceFlatNumber, 
                  PostalCode: SourcePostalCode, 
                  City: SourceCity},
                DestinationAddress: {
                  Street: DestinationStreet, 
                  StreetNumber: DestinationStreetNumber, 
                  FlatNumber: DestinationFlatNumber, 
                  PostalCode: DestinationPostalCode, 
                  City: DestinationCity}}),
              timeout: 30000,
          });

          const responseData = await response.text();

          const responseStatus = response.status;
          
          if(responseStatus === 200)
          {
            navigate('/couriersList');
          }
          else
          {
            console.log(JSON.stringify(responseData));
            setOnErrorMessage(`Provided data is invalid ${responseData}`);
          }

      } catch (error) {
          //console.error('Błąd:', error);
      }
      setIsLoading(false);
  }

if(isLoading)
{
  return (
    <LoadingPage/>
  );
}

    return (
      <div>
        {isLoading ? (<div>
        <LoadingPage/>
        </div>) : (
          <div className="App-header">
        <form onSubmit={handleSubmit}>
        <FormControl fullWidth>
            <Grid container spacing={2}>
                <Grid item>
                <Box component="section" sx={{ p: 2, border: '1px solid grey', borderRadius: 8, m: 3}}>
          <FormControl variant='outlined'>
            <FormLabel >Package details</FormLabel>
            <TextField
              label="Length"
              variant="outlined"
              margin="normal"
              fullWidth
              required
              value={Length}
              onChange={(e)=>setLength(e.target.value)}
            />
            <TextField
              label="Width"
              variant="outlined"
              margin="normal"
              fullWidth
              required
              value={Width}
              onChange={(e)=>setWidth(e.target.value)}
            />
            <TextField
              label="Height"
              variant="outlined"
              margin="normal"
              fullWidth
              required
              value={Height}
              onChange={(e)=>setHeight(e.target.value)}
            />
            <TextField
              label="Weight"
              variant="outlined"
              margin="normal"
              fullWidth
              required
              value={Weight}
              onChange={(e)=>setWeight(e.target.value)}
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
              disabled={hiddenSourceStreet}
              onChange={(e)=>setSourceStreet(e.target.value)}
            />
            <TextField
              label="Street Number"
              variant="outlined"
              margin="normal"
              fullWidth
              required
              value={SourceStreetNumber}
              disabled={hiddenSourceStreetNumber}
              onChange={(e)=>setSourceStreetNumber(e.target.value)}
            />
            <TextField
              label="Flat Number"
              variant="outlined"
              margin="normal"
              fullWidth
              required
              value={SourceFlatNumber}
              disabled={hiddenSourceFlatNumber}
              onChange={(e)=>setSourceFlatNumber(e.target.value)}
            />
            <TextField
              label="Postal Code"
              variant="outlined"
              margin="normal"
              fullWidth
              required
              value={SourcePostalCode}
              disabled={hiddenSourcePostalCode}
              onChange={(e)=>setSourcePostalCode(e.target.value)}
            />
            <TextField
              label="City"
              variant="outlined"
              margin="normal"
              fullWidth
              required
              value={SourceCity}
              disabled={hiddenSourceCity}
              onChange={(e)=>setSourceCity(e.target.value)}
            />
          </FormControl>
          </Box>
                </Grid>
                <Grid item>
                <Box component="section" sx={{ p: 2, border: '1px solid grey', borderRadius: 8, m: 3}}>
          <FormControl variant='outlined'>
            <FormLabel>Destination Address</FormLabel>
            <TextField
              label="Street"
              variant="outlined"
              margin="normal"
              fullWidth
              required
              value={DestinationStreet}
              onChange={(e)=>setDestinationStreet(e.target.value)}
            />
            <TextField
              label="Street Number"
              variant="outlined"
              margin="normal"
              fullWidth
              required
              value={DestinationStreetNumber}
              onChange={(e)=>setDestinationStreetNumber(e.target.value)}
            />
            <TextField
              label="Flat Number"
              variant="outlined"
              margin="normal"
              fullWidth
              required
              value={DestinationFlatNumber}
              onChange={(e)=>setDestinationFlatNumber(e.target.value)}
            />
            <TextField
              label="Postal Code"
              variant="outlined"
              margin="normal"
              fullWidth
              required
              value={DestinationPostalCode}
              onChange={(e)=>setDestinationPostalCode(e.target.value)}
            />
            <TextField
              label="City"
              variant="outlined"
              margin="normal"
              fullWidth
              required
              value={DestinationCity}
              onChange={(e)=>setDestinationCity(e.target.value)}
            />
          </FormControl>
          </Box>
                </Grid>

                <Grid item>
                <Box component="section" sx={{ p: 2, border: '1px solid grey', borderRadius: 8, m: 3}}>
          <FormControl variant='outlined'>
            <FormLabel >Delivery and pickup date</FormLabel>
            
            <LocalizationProvider dateAdapter={AdapterDayjs}>
              <DatePicker value={DateFrom}               
              onChange={(newDate) => setDateFrom(newDate)}
              renderInput={(params) => <TextField {...params} /> }/>
            </LocalizationProvider>

            <LocalizationProvider dateAdapter={AdapterDayjs}>
              <DatePicker 
              value={DateTo}           
              onChange={(newDate) => setDateTo(newDate)}
              renderInput={(params) => <TextField {...params} /> }/>
            </LocalizationProvider>
            <FormControlLabel control={<Checkbox value={DeliveryAtWeekend} defaultChecked
              onChange={(e)=>{setDeliveryAtWeekend(e.target.checked);}}/>} sx={{ color: 'text.secondary', fontWeight: 'bold' }} label="Delivery at weekend" />

          </FormControl>
          </Box>
                </Grid>

            </Grid>
          
        
  
          <Box component="section" sx={{ p: 2, border: '1px solid grey', borderRadius: 8, m: 3, width: '40%',
                            marginLeft: 'auto', marginRight: 'auto'}}>
            <FormControl fullWidth>
              <InputLabel>Priority</InputLabel>
              <Select required value={Priority} onChange={(e)=>setPriority(e.target.value)}>
                <MenuItem value={0}>Low</MenuItem>
                <MenuItem value={1}>High</MenuItem>
              </Select>
            </FormControl>
          </Box>
          {onErrorMessage &&
            <Box component="section" sx={{ p: 2, border: '1px solid red', borderRadius: 8, m: 3, width: '40%', marginLeft: 'auto', marginRight: 'auto'}}>
            <Typography variant="h6" color="textSecondary">
              {onErrorMessage}
            </Typography>
          </Box>}



        <Button type="button" onClick={handleSubmit} variant="contained" sx={{color: 'white', backgroundColor: 'rgb(45, 45, 45)',}}>Submit</Button>
        
          
        </FormControl>
      </form>
      </div>
        )}
      </div>
      
      
    );
  }