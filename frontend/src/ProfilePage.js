import { useAuth0 } from "@auth0/auth0-react";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";

export const ProfilePage = () =>
{
    const { isAuthenticated, getIdTokenClaims } = useAuth0();
    const [profile, setProfile] = useState();
    const [userExists, setUserExists] = useState(false);
    const navigate = useNavigate();


    return (
      <div>
          <label>Hello {profile} </label>
      </div>);
}