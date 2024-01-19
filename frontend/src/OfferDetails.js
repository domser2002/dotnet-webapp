import { useEffect, useState } from 'react';
import { Typography, Box} from '@mui/material';
import { useStore } from './store.js';

import './OfficeWorkerPanel.css';
import './CouriersListPage.css';


export function OfferDetails() {

    const [details, setDetails] = useState();
    const {RequestId} = useStore();

    useEffect(() => {
      const fetchData = async () => {
          try {
            const response = await fetch(`https://localhost:7160/api/offers`);
            const data = await response.json();
            const filteredData = data.filter((offer) => offer["id"] === RequestId);
            console.log(filteredData[0]);
            setDetails(filteredData[0]);
          } catch (error) {
              console.error('Error fetching user data:', error);
          }
      };
  
      fetchData();
  }, [RequestId]);




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
