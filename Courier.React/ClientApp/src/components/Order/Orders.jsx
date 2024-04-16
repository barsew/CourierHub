import React, { useState, useEffect } from 'react'
import { OrdersTable } from '../CustomTable/OrdersTable';
import { useAuth0 } from "@auth0/auth0-react";

const Orders = () => {
    const { isAuthenticated } = useAuth0();

    return (
        <>
            {!isAuthenticated ?
                <h5>You must be logged in to see your Orders</h5> :
                <div style={{ textAlign: 'center', color: 'black' }}>
                    <h1>Orders List</h1>
                    <OrdersTable />
                </div>
            }
        </>
    )
}

export default Orders;

