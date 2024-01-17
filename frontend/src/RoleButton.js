import { useAuth0 } from '@auth0/auth0-react';
import { Button } from '@mui/material';
import { useState } from 'react';
import { NavLink } from 'react-router-dom';
import { useEffect } from 'react';
//import {ContentPasteSearchIcon} from '@mui/icons-material/ContentPasteSearch';

export const RoleButton = () => {
    const { isAuthenticated, getIdTokenClaims } = useAuth0();
    const [role, setRole] = useState();

    useEffect(() => {
        const getRolesFromToken = async () => {
          try {
            if (isAuthenticated) {
              const accessToken = await getIdTokenClaims();

              setRole(accessToken["role"][0]);
            }
          } catch (error) {
            console.error('Error while decoding token:', error);
          }
        };
    
        getRolesFromToken();
      }, [getIdTokenClaims, isAuthenticated]);

        if(!isAuthenticated) 
        { 
            return (
                <NavLink to={"/form"}>
                    <Button variant="contained" sx={{color: 'white', backgroundColor: 'rgb(45, 45, 45)',}}>Send Package</Button>
                </NavLink>
            );
        }
        else 
        {
            if(role === "Courier")
            {
                return (
                    <NavLink to={"/courierPanel"}>
                        <Button variant="contained" sx={{color: 'white', backgroundColor: 'rgb(45, 45, 45)',}}>Courier panel</Button>
                    </NavLink>
                );
            }
            if(role === "Office worker")
            {
                return (
                    <NavLink to={"/officeWorkerPanel"}>
                        <Button variant="contained" sx={{color: 'white', backgroundColor: 'rgb(45, 45, 45)',}}>Office worker panel</Button>
                    </NavLink>
                );
            }
        }
        return (
            <NavLink to={"/form"}>
                <Button variant="contained" sx={{color: 'white', backgroundColor: 'rgb(45, 45, 45)',}}>Send Package</Button>
            </NavLink>
        );

};
