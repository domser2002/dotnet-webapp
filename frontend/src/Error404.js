import ChromeDinoGame from 'react-chrome-dino';
import { Typography } from '@mui/material';
import "./App.css";

export function Error404()
{
    return(
        <div className='App-header'>
            <Typography variant="h2" component="div" sx={{ color: 'text.secondary', fontWeight: 'bold' }}>
                Page does not exist!
            </Typography>
            <ChromeDinoGame/>
        </div>
    );
}