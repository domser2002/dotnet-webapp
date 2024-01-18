import { useAuth0 } from "@auth0/auth0-react";
import GoogleIcon from '@mui/icons-material/Google';
import {Button, Typography} from '@mui/material';
import { NavLink } from "react-router-dom";

export const LogoutButton = () =>
{
    const {logout, isAuthenticated, user} = useAuth0();
    return (
        isAuthenticated && (
            <div style={{ display: 'flex', alignItems: 'center' }}>
                <Typography variant="h6" color="primary" fontFamily="Monospace" style={{ marginRight: '10px' }}>Hello {user?.name}!</Typography>
                <NavLink to={"/profile"}>
                    <img src={user?.picture} alt='profilePicture' style={{ width: '40px', height: '40px', marginRight: '10px' }}></img>
                </NavLink>
                
                <Button 
                    onClick={()=> logout()}
                    variant="contained" 
                    sx={{color: 'white', backgroundColor: 'rgb(45, 45, 45)',}}
                    color="primary"
                    startIcon={<GoogleIcon />}
                    >
                    Logout
                </Button>
            </div>

        )

    )
}