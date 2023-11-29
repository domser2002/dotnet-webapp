import {NavLink, Outlet} from "react-router-dom";
import './App.css';
import {Button} from '@mui/material';

export function Layout() {
    return(
        <div id="lay" >
            <div className='Navibar' style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <div>
                    <NavLink to={"/"}>
                        <Button variant="contained" sx={{color: 'white', backgroundColor: 'rgb(45, 45, 45)',}}>Main page</Button>
                    </NavLink>
                    <span>{'     '}</span>
                    <NavLink to={"/form"}>
                        <Button variant="contained" sx={{color: 'white', backgroundColor: 'rgb(45, 45, 45)',}}>Send Package</Button>
                    </NavLink>
                    <NavLink to={"/contactInformation"}>
                        <Button variant="contained" sx={{color: 'white', backgroundColor: 'rgb(45, 45, 45)',}}>Contact information</Button>
                    </NavLink>
                </div>
                <div>
                    <NavLink to={"/login"}>
                        <Button variant="contained" sx={{color: 'white', backgroundColor: 'rgb(45, 45, 45)',}}>Login</Button>
                    </NavLink>
                    <NavLink to={"/register"}>
                        <Button variant="contained" sx={{color: 'white', backgroundColor: 'rgb(45, 45, 45)',}}>Register</Button>
                    </NavLink>
                </div>

            </div>

            <Outlet/>
        </div>
    );
}
