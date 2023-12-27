import { useEffect, useState } from 'react';
import { Typography, Box} from '@mui/material';
import { useStore } from './store.js';
import { useAuth0 } from '@auth0/auth0-react';

import './OfficeWorkerPanel.css';
import './CouriersListPage.css';


export function OfferDetails() {

    const baseUrl = process.env.REACT_APP_API_URL;
    const apiOffers = baseUrl+"/api/offers";

    const [details, setDetails] = useState();
    const {RequestId} = useStore();

    const {getIdTokenClaims} = useAuth0();

    useEffect(() => {
      const fetchData = async () => {
          try {
            const claims = await getIdTokenClaims();
            const response = await fetch(apiOffers, {
              headers: {
                  Authorization: `Bearer ${claims["__raw"]}`,

              },
          });
            const data = await response.json();
            const filteredData = data.filter((offer) => offer["id"] === RequestId);
            console.log(filteredData[0]);
            setDetails(filteredData[0]);
          } catch (error) {
              console.error('Error fetching user data:', error);
          }
      };
  
      fetchData();
  }, [RequestId, apiOffers, getIdTokenClaims]);




    return (
      <div className="Panel-header-officeWorker">
        {details && (
        
        <Box>

          <Typography variant="body1" gutterBottom className="gray-text">
            Company Name: {details.companyName}
          </Typography>

          <Typography variant="body1" gutterBottom className="gray-text">
            Price: {details.price}
          </Typography>

          <Typography variant="body1" gutterBottom className="gray-text">
            Active: {details.active ?  'Yes' : 'No'}
          </Typography>

          <Typography variant="body1" gutterBottom className="gray-text">
            Delivery time: {details.deliveryTime}
          </Typography>

          <Typography variant="body1" gutterBottom className="gray-text">
            Begin date: {details.begins.split('T')[0]}
          </Typography>
          <Typography variant="body1" gutterBottom className="gray-text">
            End date: {details.ends.split('T')[0]}
          </Typography>

          <Typography variant="body1" gutterBottom className="gray-text">
            Package - minimal dimension: {details.minDimension}
          </Typography>
          <Typography variant="body1" gutterBottom className="gray-text">
            Package - maximal dimension: {details.maxDimension}
          </Typography>
          <Typography variant="body1" gutterBottom className="gray-text">
            Package - minimal weight: {details.minWeight}
          </Typography>
          <Typography variant="body1" gutterBottom className="gray-text">
            Package - maximal weight: {details.maxWeight}
          </Typography>
        </Box>
        
      )}
     
    </div>
    );
}
