import { useAuth0 } from "@auth0/auth0-react";
import { useState } from "react";

export const useRole = async () => {

    const { isAuthenticated, getIdTokenClaims } = useAuth0();
    const [role, setRole] = useState("User");
    
    try {
        if (isAuthenticated) {
            const accessToken = await getIdTokenClaims();
            setRole(accessToken["role"][0]);
        }
        else
        {
            return setRole("User");
        }
    } 
    catch (error) {
        console.error('Error while decoding token:', error);
    }

    return role;
  };