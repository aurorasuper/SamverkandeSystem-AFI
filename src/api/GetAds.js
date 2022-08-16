import React from 'react';
import { useState } from 'react';
import axios from 'axios';

function GetAds() {
    const [ads, setAds] = useState({})

    axios.get('https://localhost:7015/api/Ads')
            .then(res => {
                setAds(res.data)
            }).catch(err => {
                return err;
            })
    
    return ads;
}

export default GetAds;