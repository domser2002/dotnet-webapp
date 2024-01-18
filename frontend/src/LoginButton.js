import { useAuth0 } from "@auth0/auth0-react";
import { Button } from "@mui/material";
import GoogleIcon from '@mui/icons-material/Google';

export const LoginButton = () =>
{
    const {loginWithRedirect, isAuthenticated} = useAuth0();
    return (
        !isAuthenticated && (
            <Button 
            onClick={()=> loginWithRedirect()}
            variant="contained" 
            sx={{color: 'white', backgroundColor: 'rgb(45, 45, 45)',}}
            color="primary"
            startIcon={<GoogleIcon />}
            >
                Login by ClientID
            </Button>
        )

    )
}