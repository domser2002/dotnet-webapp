import React from 'react';
import { useStore } from './store'; // Ustaw właściwą ścieżkę
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button } from '@mui/material';
import { LoadingPage } from './LoadingPage';
import { useAuth0 } from '@auth0/auth0-react';

import './SummaryPage.css';

export const SummaryPage = () => {
  
  const baseUrl = process.env.REACT_APP_API_URL;
  const apiOffers = baseUrl+"/api/offers";
  const apiRequests = baseUrl+"/api/requests";
  
  const {
    offerId,
    SourceStreet,
    SourceStreetNumber,
    SourceFlatNumber,
    SourcePostalCode,
    SourceCity,
    DestinationStreet,
    DestinationStreetNumber,
    DestinationFlatNumber,
    DestinationPostalCode,
    DestinationCity,
    DeliveryAtWeekend,
    Priority,
    Length,
    Width,
    Height,
    Weight,
    DateFrom,
    DateTo,
    PersonaData,
    Email,
    CompanyName,
    OwnerSourceStreet,
    OwnerSourceStreetNumber,
    OwnerSourceFlatNumber,
    OwnerSourcePostalCode,
    OwnerSourceCity,
  } = useStore();

  const [offerData, setOfferData] = useState(null);
  const [isLoading, setIsLoading] = useState(true);
  const navigate = useNavigate();

  const {getIdTokenClaims} = useAuth0();

  useEffect(() => {
    setIsLoading(true);
    const fetchOfferData = async () => {
      try {
        const claims = await getIdTokenClaims();
        const response = await fetch(`${apiOffers}/${offerId}`, {
          headers: {
              Authorization: `Bearer ${claims["__raw"]}`,
              // Dodaj inne nagłówki, jeśli są potrzebne
          },
      });
        const data = await response.json();
        setOfferData(data);
        setIsLoading(false);
      } catch (error) {
        console.error('Error fetching offer data:', error);
        setIsLoading(false);
      }
    };

    if (offerId) {
      fetchOfferData();
    }
  }, [offerId, apiOffers, getIdTokenClaims]);

  const handleSubmit = async () => {
    setIsLoading(true);
    try {
      const claims = await getIdTokenClaims();
        const response = await fetch(apiRequests, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${claims["__raw"]}`,
            },
            body: JSON.stringify({
              Package: {
                Length: Length, 
                Width: Width, 
                Height: Height, 
                Weight: Weight
              },
              SourceAddress: {
                Street: SourceStreet, 
                StreetNumber: SourceStreetNumber, 
                FlatNumber: SourceFlatNumber, 
                PostalCode: SourcePostalCode, 
                City: SourceCity},
              DestinationAddress: {
                Street: DestinationStreet, 
                StreetNumber: DestinationStreetNumber, 
                FlatNumber: DestinationFlatNumber, 
                PostalCode: DestinationPostalCode, 
                City: DestinationCity},
              PickupDate: DateFrom.toISOString(),
              DeliveryDate: DateTo.toISOString(),
              CompanyName: offerData["companyName"],
              Price: offerData["price"],
              Status: 0,
              Owner: {
                PersonalData: PersonaData,
                Email: Email,
                Address: {
                    Street: OwnerSourceStreet, 
                    StreetNumber: OwnerSourceStreetNumber, 
                    FlatNumber: OwnerSourceFlatNumber, 
                    PostalCode: OwnerSourcePostalCode, 
                    City: OwnerSourceCity},
              },
            }),
        });

        const responseData = await response.text();

        const responseStatus = response.status;
        
        if(responseStatus === 201)
        {
          navigate('/profile');
        }

    } catch (error) {
        console.error('Błąd:', error);
    }
    setIsLoading(false);
}

  if(isLoading)
  {
    return(
        <LoadingPage/>
    );
  }

  return (
    <div className='summary-header'>
        <Box
        sx={{
            marginTop: 2,
            padding: 2,
            border: 1,
            borderRadius: 1,
            borderColor: 'primary.main',
            backgroundColor: 'primary.light',
        }}
        >
            <Typography variant="h5">Summary</Typography>
            <Typography>
                Source Address: {SourceStreet} {SourceStreetNumber}, {SourceFlatNumber}, {SourcePostalCode} {SourceCity}
            </Typography>
            <Typography>
                Destination Address: {DestinationStreet} {DestinationStreetNumber}, {DestinationFlatNumber}, {DestinationPostalCode} {DestinationCity}
            </Typography>
            <Typography>Delivery at Weekend: {DeliveryAtWeekend ? 'Yes' : 'No' }</Typography>
            <Typography>Priority: {Priority === 0 ? 'Low' : 'High'}</Typography>
            <Typography>Dimensions: {Length} x {Width} x {Height}</Typography>
            <Typography>Weight: {Weight}</Typography>
            <Typography>Pickup date: {DateFrom && new Date(DateFrom).toLocaleDateString()}</Typography>
            <Typography>Delivery date: {DateTo && new Date(DateTo).toLocaleDateString()}</Typography>
            <Typography>Cancellation date: {DateTo && new Date(DateTo).toLocaleDateString()}</Typography>
            <Typography>Company Name: {offerData["companyName"]}</Typography>
            <Typography>Price: {offerData["price"]}</Typography>
            <Typography>Personal data: {PersonaData}</Typography>
            <Typography>Your company: {CompanyName}</Typography>
            <Typography>Email: {Email}</Typography>
            <Typography>Address: {OwnerSourceStreet}, {OwnerSourceStreetNumber}, {OwnerSourceFlatNumber},
             {OwnerSourcePostalCode}, {OwnerSourceCity}</Typography>
             <Button type="button" onClick={handleSubmit} variant="contained"
                sx={{color: 'white', backgroundColor: 'rgb(45, 45, 45)',}}>Submit request</Button>

        </Box>
    </div>
  );
};
