import { useEffect, useState } from 'react';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText'
import { Button, Typography, TextField} from '@mui/material';
import { useAuth0 } from '@auth0/auth0-react';
import { useNavigate } from 'react-router-dom';
import { useStore } from './store';

import './CouriersListPage.css';
import "./OfficeWorkerPanel.css";


export function CompanyOffers() {

    const [requests, setRequests] = useState([]);
    const [ setCompany ] = useState();
    const {getIdTokenClaims} = useAuth0();
    const navigate = useNavigate();
    const {setRequestId} = useStore();
    useEffect(() => {
      const fetchUserData = async () => {
          try {
              const claims = await getIdTokenClaims();
              const id = claims["sub"].split('|')[1];
              const response = await fetch(`https://localhost:7160/api/users/subs/${id}`);
              const data = await response.json();
              return data["companyName"];

          } catch (error) {
              console.error('Error fetching user data:', error);
          }
      };
  
      const fetchRequestsData = async (d) => {
          try {
              const response = await fetch(`https://localhost:7160/api/offers`);
              const data = await response.json();
              const filteredRequests = data.filter((offer) => offer["companyName"] === d);

              setRequests(filteredRequests);
          } catch (error) {
              console.error('Error fetching requests data:', error);
          }
      };
  
      const fetchData = async () => {
          const d = await fetchUserData();
          await fetchRequestsData(d);
      };
  
      fetchData();
  }, [setCompany, setRequests, getIdTokenClaims]);


    const handleDetailsClick = async (id) => 
    {
      setRequestId(id);
      navigate("./offerDetails");
    }

    const [sortBy, setSortBy] = useState(null);

  const handleSortBy = (sortField) => {
    if (sortBy === sortField) {
      // Jeśli kliknięty guzik sortowania jest już aktywny, zmień kierunek sortowania
      setRequests([...requests.reverse()]);
    } else {
      // Sortuj po wybranym polu
      let sortedRequests = [...requests];
      switch (sortField) {
        case 'pickupDate':
          sortedRequests.sort((a, b) => new Date(a.pickupDate) - new Date(b.pickupDate));
          break;
        case 'deliveryDate':
          sortedRequests.sort((a, b) => new Date(a.deliveryDate) - new Date(b.deliveryDate));
          break;
        case 'status':
          sortedRequests.sort((a, b) => a.status - b.status);
          break;
        default:
          break;
      }
      setRequests(sortedRequests);
    }
    setSortBy(sortField);
  };


  const [filterDate, setFilterDate] = useState(null);
  const [filterDeliveryDate, setDeliveryFilterDate] = useState(null);

  const handleFilterDateChange = (event) => {
    const selectedDate = event.target.value;
    setFilterDate(selectedDate);
  };

  const handleFilterDeliveryDateChange = (event) => {
    const selectedDate = event.target.value;
    setDeliveryFilterDate(selectedDate);
  };


  const resetFilters = () => {
    setFilterDate(null);
    setDeliveryFilterDate(null);
  };

  const filteredRequests = requests.filter((offer) => {
    return (
      (!filterDate || offer.pickupDate.split('T')[0] === filterDate ) &&
      (!filterDeliveryDate || offer.deliveryDate.split('T')[0] === filterDeliveryDate )

    );
  });

    return (
      <div className="Panel-header-officeWorker">
          <Typography variant="h3" gutterBottom className='gray-text'>
            Your company's requests
          </Typography>
          <Typography variant="h5" gutterBottom className='gray-text'>
            Sorting
          </Typography>
        <div className="sort-buttons">
          <Button
            variant="contained"
            color="primary"
            onClick={() => handleSortBy('pickupDate')}
          >
            Sort by Begin Date
          </Button>
          <Button
            variant="contained"
            color="primary"
            onClick={() => handleSortBy('deliveryDate')}
          >
            Sort by End Date
          </Button>
      </div>
      <Typography variant="h5" gutterBottom className='gray-text'>
            Filtering
          </Typography>
      <div className="button-container">
        <TextField
          id="filter-date"
          label="Filter by pickup date"
          type="date"
          variant="outlined"
          InputLabelProps={{
            shrink: true,
          }}
          value={filterDate || ''}
          onChange={handleFilterDateChange}
        />
        <TextField
          id="filter-date"
          label="Filter by delivery date"
          type="date"
          variant="outlined"
          InputLabelProps={{
            shrink: true,
          }}
          value={filterDate || ''}
          onChange={handleFilterDeliveryDateChange}
        />

        <Button
          variant="contained"
          color="primary"
          onClick={resetFilters}
        >
          Reset Filters
        </Button>
      </div>
      <List>
        {filteredRequests.map((offer) => (
          <ListItem key={offer.id} className="list-item">
            <ListItemText
              primary={`ID: ${offer.id}`}
              secondary={`Date: ${offer.begins && new Date(offer.begins).toLocaleDateString()} - 
                  ${offer.ends && new Date(offer.ends).toLocaleDateString()}`}
              className="list-item-text"
            />
            <Button
              variant="contained"
              color="primary"
              className="list-item-button"
              onClick={() => handleDetailsClick(offer.id)}
            >
              Details
            </Button>
          </ListItem>
        ))}
      </List>

      </div>
    );

}