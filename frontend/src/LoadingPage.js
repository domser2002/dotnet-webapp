import { Typography } from '@mui/material';
import './LoadingPage.css'
import './App.css'

export function LoadingPage()
{
    return(
        <div className='App-header'>
            <div className='rotating-image'>
                <img src='./wydraTransparent.png' alt='wydra' style={{ width: '200px', height: '200px' }}></img>
            </div>
            <Typography variant="h2" component="div" sx={{ color: 'text.secondary', fontWeight: 'bold' }}>
                Loading...
            </Typography>
        </div>
    );
}