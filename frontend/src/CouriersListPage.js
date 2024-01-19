//import axios from 'axios';
import { useEffect, useState } from 'react';
//const url = 'https://localhost:7160/api/offers';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText'
import './CouriersListPage.css';
import { Button } from '@mui/material';
import { useAuth0 } from '@auth0/auth0-react';
import { useNavigate } from 'react-router-dom';
import { useStore } from './store';
import { LoadingPage } from './LoadingPage';

export function CouriersListPage() {

    const baseUrl = process.env.REACT_APP_API_URL;
    const usersSubsId = baseUrl+"/api/users/subs";
    const usersOffer = baseUrl+"/api/offers";

    const {
      setPersonaData,
      setEmail,
      setCompanyName,
      setOwnerSourceStreet,
      setOwnerSourceStreetNumber,
      setOwnerSourceFlatNumber,
      setOwnerSourcePostalCode,
      setOwnerSourceCity,
    } = useStore();
    const setOfferId = useStore((state) => state.setOfferId);
    const [offers, setOffers] = useState([]);
    const {isAuthenticated, getIdTokenClaims} = useAuth0();
    const navigate = useNavigate();
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
      setIsLoading(true);
      const fetchData = async () => {
        try {
          const response = await fetch(usersOffer);
          const data = await response.json();
          setOffers(data);
        } catch (error) {
          console.error('GET error:', error);
        }
      };
    
      const fetchUserData = async () => {
        if (isAuthenticated) {
          try {
            const claims = await getIdTokenClaims();
            const id = claims["sub"].split('|')[1]
            const response = await fetch(`${usersSubsId}/${id}`);
    
            if (response.ok) {
              const userData = await response.json();
    
              console.log(userData["fullName"]);
              setPersonaData(userData["fullName"]);
              setEmail(userData["email"]);
              setCompanyName(userData["companyName"]);
              setOwnerSourceStreet(userData["defaultSourceAddress"]["street"]);
              setOwnerSourceStreetNumber(userData["defaultSourceAddress"]["streetNumber"]);
              setOwnerSourceFlatNumber(userData["defaultSourceAddress"]["flatNumber"]);
              setOwnerSourcePostalCode(userData["defaultSourceAddress"]["postalCode"]);
              setOwnerSourceCity(userData["defaultSourceAddress"]["city"]);
    
            } else {
              console.error('Błąd podczas pobierania danych użytkownika:', response.statusText);
            }
          } catch (error) {
            console.error('Błąd podczas pobierania danych użytkownika:', error.message);
          }
        }
      };
    
      fetchData();
      fetchUserData();
      setIsLoading(false);
    }, [isAuthenticated, getIdTokenClaims, setPersonaData, setEmail, setCompanyName,usersSubsId,usersOffer,
       setOwnerSourceStreet, setOwnerSourceStreetNumber, setOwnerSourceFlatNumber, setOwnerSourcePostalCode, setOwnerSourceCity]);
    

    const handleButtonClick = async (id) => 
    {
      setOfferId(id);
      if(!isAuthenticated)
      {
        navigate("/contactInformation");
      }
      else
      {
        navigate("/summaryPage");
      }

    }

    if(isLoading)
    {
      return(
        <LoadingPage/>
      );
    }
    return (
    <div className="CouriersListPage-header">
      <List>
        {offers.map((offer) => (
          <ListItem key={offer.id}>
            <ListItemText
              primary={offer.companyName} 
              secondary={`Price: ${offer.price}`} 
            />
            <Button
              variant="contained"
              color="primary"
              style={{ backgroundColor: '#0d10a6', color: 'white' }}
              onClick={() => handleButtonClick(offer.id)}
            >Choose
            </Button>
          </ListItem>
        ))}
      </List>

    </div>
    );
}