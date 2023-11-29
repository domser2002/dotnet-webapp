import './App.css';
import React, { useState } from 'react';
import { Grid, Checkbox, FormControl, InputLabel, Select, MenuItem, TextField, Button, FormLabel, FormControlLabel, Box } from '@mui/material';
import { useNavigate } from 'react-router-dom';
//import { DatePicker, LocalizationProvider } from '@mui/x-date-pickers';
// import DatePicker from '@mui/lab/DatePicker';
// import AdapterDateFns from '@mui/lab/AdapterDateFns';
// import LocalizationProvider from '@mui/x-date-pickers';
export function FormPage() {
    const navigate = useNavigate();

    const handleSubmit = async () => {
      try {
          const response = await fetch('https://localhost:7160/api/inquires', {
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
                PickupDate: DateFrom,
                DeliveryDate: DateTo,
                SourceAddress: {
                  Street: SourceStreet, 
                  StreetNumber: SourceStreetNumber, 
                  FlatNumber: SourceFlatNumber, 
                  PostalCode: SourcePostalCode, 
                  City: SourceCity},
                DeliveryAddress: {
                  Street: DestinationStreet, 
                  StreetNumber: DestinationStreetNumber, 
                  FlatNumber: DestinationFlatNumber, 
                  PostalCode: DestinationPostalCode, 
                  City: DestinationCity}}),
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


    const [SourceStreet, setSourceStreet] = useState("");
    const [SourceStreetNumber, setSourceStreetNumber] = useState("");
    const [SourceFlatNumber, setSourceFlatNumber] = useState("");
    const [SourcePostalCode, setSourcePostalCode] = useState("");
    const [SourceCity, setSourceCity] = useState("");

    const [DestinationStreet, setDestinationStreet] = useState("");
    const [DestinationStreetNumber, setDestinationStreetNumber] = useState("");
    const [DestinationFlatNumber, setDestinationFlatNumber] = useState("");
    const [DestinationPostalCode, setDestinationPostalCode] = useState("");
    const [DestinationCity, setDestinationCity] = useState("");

    const [DeliveryAtWeekend, setDeliveryAtWeekend] = useState(true);

    const [Priority, setPriority] = useState(0);

    const [Length, setLength] = useState("");
    const [Width, setWidth] = useState("");
    const [Height, setHeight] = useState("");
    const [Weight, setWeight] = useState("");

    const [DateFrom, setDateFrom] = useState("");
    const [DateTo, setDateTo] = useState("");

    return (
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
            
            <TextField
              label="Pickup date"
              variant="outlined"
              margin="normal"
              fullWidth
              required
              value={DateFrom}
              onChange={(e)=>setDateFrom(e.target.value)}
            />
            
            <TextField
              label="Delivery date"
              variant="outlined"
              margin="normal"
              fullWidth
              required
              value={DateTo}
              onChange={(e)=>setDateTo(e.target.value)}
            />

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
  
  
  
          <FormControlLabel control={<Checkbox value={DeliveryAtWeekend} defaultChecked
          onChange={(e)=>{setDeliveryAtWeekend(e.target.checked);}}/>} label="Delivery at weekend" />
        {/* <NavLink to={"/couriersList"}></NavLink> */}
        <Button type="submit" variant="contained" sx={{color: 'white', backgroundColor: 'rgb(45, 45, 45)',}}>Submit</Button>
        
          
        </FormControl>
      </form>
      </div>
    );
  }