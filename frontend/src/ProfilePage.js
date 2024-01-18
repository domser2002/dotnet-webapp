import { useAuth0 } from "@auth0/auth0-react";
import { useState, useEffect } from "react";

export const ProfilePage = () =>
{

    const { isAuthenticated, getIdTokenClaims } = useAuth0();
    const [profileId, setProfileId] = useState();
    //const [user, setUser] = useState();
    useEffect(() => {
        const getIdFromToken = async () => {
          try {
            if (isAuthenticated) {
              const accessToken = await getIdTokenClaims();
    
              const newProfileId = accessToken["sub"].split('|')[1].slice(-2);
              setProfileId(newProfileId);
    
              const resp = await getUserById(newProfileId);
              if (resp) {

              } else {
                await handleSaveUser(newProfileId);
              }
            }
          } catch (error) {
            console.error('Error while decoding token:', error);
          }
        };
    
        const getUserById = async (id) => {
          try {
            const response = await fetch(`https://localhost:7160/api/users/${id}`);
            return response.status === 200;
          } catch (error) {
            console.error('Error fetching user:', error);
            return false;
          }
        };
    
        const handleSaveUser = async (id) => {
          try {
            const response = await fetch(`https://localhost:7160/api/users`, {
              method: 'POST',
              headers: {
                'Content-Type': 'application/json',
              },
              body: JSON.stringify({
                Id: id,
                FirstName: "aaaa",
                LastName: "aaaa",
                CompanyName: "cccccc",
                Email: "mdp.ch3@gmail.com",
                Address: {
                    Street: "SourceStreet", 
                    StreetNumber: "0", 
                    FlatNumber: "0", 
                    PostalCode: "26-706", 
                    City: "SourceCity"},
                  DefaultSourceAddress: {
                    Street: "SourceStreet", 
                    StreetNumber: "0", 
                    FlatNumber: "0", 
                    PostalCode: "26-706", 
                    City: "SourceCity"},
              }),
            });
    
            if (response.ok) {
              console.log('User created successfully');
              // Tutaj możesz dodać dodatkową logikę po zapisaniu użytkownika
            } else {
              console.error('Failed to create user:', response.statusText);
            }
          } catch (error) {
            console.error('Error while creating user:', error);
          }
        };
    
        getIdFromToken();
    
      }, [getIdTokenClaims, isAuthenticated]);
      console.log(profileId);
    return (
        <div>
            <label>Profile page </label>
        </div>
    );

    
}