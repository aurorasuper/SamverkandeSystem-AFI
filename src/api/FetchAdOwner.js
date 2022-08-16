import React from "react";
import axios from "axios";

const fetchAdOwner = (id) => {
    React.state = {
        result: {}
    }

    axios.get('https://localhost:7015/api/AdOwner/'+id)
            .then(res => {
                this.state.result = res;
            }).catch(err => {
                return err;
            })
    return this.state;
}

export default fetchAdOwner;