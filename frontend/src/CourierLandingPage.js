import './App.css';
import React from 'react';
import { Typography } from '@mui/material';


export function CourierLandingPage() {
    
    return (
        <div className='App-header-courier'>
            <div>
                <img src='./wydraTransparent.png' alt='wydra' style={{ width: '200px', height: '200px' }}></img>
            </div>
            <div style={{ textAlign: 'center' }}>
                <Typography variant="h1" component="div" sx={{ color: 'primary.main', fontWeight: 'bold' }}>
                    Courier Hub
                </Typography>
                <Typography variant="h3" component="div" sx={{ color: 'primary.main', fontWeight: 'bold' }}>
                    for Couriers
                </Typography>
                <Typography variant="h6" sx={{ color: 'text.secondary', fontWeight: 'bold' }}>
                    by Zuzia Wójtowicz, Dominik Seredyn and Mati Chmurzyński
                </Typography>
            </div>
        </div>
    );
  }