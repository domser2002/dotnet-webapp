import './App.css';
import React from 'react';
import {Button, Typography} from '@mui/material';
import {NavLink} from "react-router-dom";

export function LandingPage() {
    return (
        <div className='App-header'>
            <div>
                <Typography variant="h1" component="div" sx={{ color: 'primary.main', fontWeight: 'bold' }}>
                    Courier Hub
                </Typography>
        
                <Typography variant="h6" sx={{ color: 'text.secondary', fontWeight: 'bold' }}>
                    by Zuzia Wójtowicz, Dominik Seredyn i Mati Chmurzyński
                </Typography>
            </div>

            <NavLink to={"/form"}>
                <Button variant="contained" sx={{margin: 5}}>Send package</Button>
            </NavLink>
        </div>
        
    );
  }