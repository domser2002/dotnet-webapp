import './App.css';
import React, { useEffect, useState } from 'react';
import {Button, Typography} from '@mui/material';
import {NavLink} from "react-router-dom";

export function LandingPage() {
    const [usersCount, setUsersCount] = useState(0);

    const baseUrl = process.env.REACT_APP_API_URL;
    const usersGetCountUrl = baseUrl+"/api/users/count";
    useEffect(() => {
        fetch(usersGetCountUrl).then(response => response.json()).then(data => {setUsersCount(data);})
        .catch(error => {
        console.error('GET error:', error);
        });
    }, [usersGetCountUrl]);
    return (
        <div className='App-header'>
            <div>
                <img src='./wydraTransparent.png' alt='wydra' style={{ width: '200px', height: '200px' }}></img>
            </div>
            <div style={{ textAlign: 'center' }}>
                <Typography variant="h1" component="div" sx={{ color: 'primary.main', fontWeight: 'bold' }}>
                    Courier Hub
                </Typography>
        
                <Typography variant="h6" sx={{ color: 'text.secondary', fontWeight: 'bold' }}>
                    by Zuzia Wójtowicz, Dominik Seredyn and Mati Chmurzyński
                </Typography>
                <br></br>
                <Typography variant="h5" sx={{ color: 'primary.main', fontWeight: 'bold' }}>
                    Currently used by {usersCount === 0 ? "many" : usersCount } people!
                </Typography>
            </div>

            <NavLink to={"/form"}>
                <Button variant="contained" sx={{margin: 5}}>Send package</Button>
            </NavLink>
        </div>
        
    );
  }