import { useEffect, useState } from 'react';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText'
import { Button, Typography, TextField, MenuItem} from '@mui/material';
import { useAuth0 } from '@auth0/auth0-react';
import { useNavigate } from 'react-router-dom';
import { useStore } from './store';

import './CouriersListPage.css';
import './CourierPanel.css';


export function CourierPanel() {

    const baseUrl = process.env.REACT_APP_API_URL;
    const usersSubsId = baseUrl+"/api/users/subs";
    const companiesName = baseUrl+"/companies";

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

              const response = await fetch(`${usersSubsId}/${id}`,
               {
                headers: {
                    Authorization: `Bearer ${claims["__raw"]}`,
                },
            }
              );
              const data = await response.json();
              return data["companyName"];

          } catch (error) {
              console.error('Error fetching user data:', error);
          }
      };
  
      const fetchRequestsData = async (d) => {
          try {
            const claims = await getIdTokenClaims();

            //const id = claims["sub"].split('|')[1];
              const response = await fetch(`${companiesName}/${d}`, {
                headers: {
                    Authorization: `Bearer ${claims["__raw"]}`,
                },
            }
              );
              const data = await response.json();
              setRequests(data);
          } catch (error) {
              console.error('Error fetching requests data:', error);
          }
      };
  
      const fetchData = async () => {
          const d = await fetchUserData();
          await fetchRequestsData(d);
      };
  
      fetchData();
  }, [setCompany, setRequests, getIdTokenClaims, usersSubsId, companiesName]);


    const handleDetailsClick = async (id) => 
    {
      setRequestId(id);
      navigate("./courierRequestDetails");
    }

    const getStatusText = (status) => {
      switch (status) {
        case 0:
          return 'Pending';
        case 1:
          return 'Accepted';
        case 2:
          return 'Received';
        case 3:
          return 'Delivered';
        case 4:
          return 'Cannot Deliver';
        case 5:
          return 'Cancelled';
        default:
          return 'Unknown Status';
      }
    };

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
  const [filterStatus, setFilterStatus] = useState(null);

  const handleFilterDateChange = (event) => {
    const selectedDate = event.target.value;
    setFilterDate(selectedDate);
  };

  const handleFilterDeliveryDateChange = (event) => {
    const selectedDate = event.target.value;
    setDeliveryFilterDate(selectedDate);
  };

  const handleFilterStatusChange = (event) => {
    const selectedStatus = event.target.value;
    setFilterStatus(selectedStatus);
  };

  const resetFilters = () => {
    setFilterDate(null);
    setDeliveryFilterDate(null);
    setFilterStatus(null);
  };

  const filteredRequests = requests.filter((offer) => {
    return (
      (!filterDate || offer.pickupDate.split('T')[0] === filterDate ) &&
      (!filterDeliveryDate || offer.deliveryDate.split('T')[0] === filterDeliveryDate ) &&
      (!filterStatus || offer.status === filterStatus)
    );
  });

    return (
      <div className="Panel-header-courier">
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
            Sort by Pickup Date
          </Button>
          <Button
            variant="contained"
            color="primary"
            onClick={() => handleSortBy('deliveryDate')}
          >
            Sort by Delivery Date
          </Button>
          <Button
            variant="contained"
            color="primary"
            onClick={() => handleSortBy('status')}
          >
            Sort by Status
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
        <TextField
          id="filter-status"
          label="Filter by Status"
          select
          variant="outlined"
          value={filterStatus || ''}
          onChange={handleFilterStatusChange}
          style={{ minWidth: '150px' }} 
        >
          <MenuItem value="" disabled>
            Select Status
          </MenuItem>
          <MenuItem value={0}>Pending</MenuItem>
          <MenuItem value={1}>Accepted</MenuItem>
          <MenuItem value={2}>Received</MenuItem>
          <MenuItem value={3}>Delivered</MenuItem>
          <MenuItem value={4}>Cannot Deliver</MenuItem>
          <MenuItem value={5}>Cancelled</MenuItem>
        </TextField>
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
              secondary={`Date: ${offer.pickupDate && new Date(offer.pickupDate).toLocaleDateString()} - 
                  ${offer.deliveryDate && new Date(offer.deliveryDate).toLocaleDateString()}`}
              className="list-item-text"
            />
            <ListItemText
              secondary={`Status: ${getStatusText(offer.status)}`}
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