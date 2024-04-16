import { useAuth0 } from "@auth0/auth0-react";
import { useEffect, useState } from "react";
import { useLocation, useParams, useNavigate } from 'react-router-dom'

const OrderTracking = () => {

    const orderId = useParams();
    const [order, setOrder] = useState(null);

    console.log(orderId);

    useEffect(() => {
        fetch(`/api2/order/details/${orderId.id}`)
            .then((result) => result.json())
            .then(data => setOrder(data))
            .catch((error) => console.error('Error fetching orders:', error));
    }, []);
    
    return (
        <div className="card m-3">
            <div className="card-header">
                <h4>Order Details</h4>
            </div>
            <div className="row m-3">
                <h5>Order Number: <span>{order?.orderId}</span></h5>

                <div className="row">
                    <div className="col">
                        <h5>Sender Name: <span>{order?.senderName}</span></h5>
                    </div>
                    <div className="col">
                        <h5>Sender Surname: <span>{order?.senderSurname}</span></h5>
                    </div>
                </div>
                <div className="row">
                    <div className="col">
                        <h5>Sender Email: <span>{order?.senderEmail}</span></h5>
                    </div>
                    <div className="col">
                        <h5>Sender Phone: <span>{order?.senderPhone}</span></h5>
                    </div>
                </div>

                <div className="row">
                    <div className="col">
                        <h5>Receiver Name: <span>{order?.receiverName}</span></h5>
                    </div>
                    <div className="col">
                        <h5>Receiver Surname: <span>{order?.receiverSurname}</span></h5>
                    </div>
                </div>
                <div className="row">
                    <div className="col">
                        <h5>Receiver Email: <span>{order?.receiverEmail}</span></h5>
                    </div>
                    <div className="col">
                        <h5>Receiver Phone: <span>{order?.receiverPhone}</span></h5>
                    </div>
                </div>

                <div className="row">
                    <h5>Source Address: <span>{order?.sourceAddress.country} {order?.sourceAddress.city} {order?.sourceAddress.zipCode} {order?.sourceAddress.street} {order?.sourceAddress.houseNumber} {order?.sourceAddress?.apartmentNumber}</span></h5>
                </div>

                <div className="row">
                    <h5>Destination Address: <span>{order?.destinationAddress.country} {order?.destinationAddress.city} {order?.destinationAddress.zipCode} {order?.destinationAddress.street} {order?.destinationAddress.houseNumber} {order?.destinationAddress?.apartmentNumber}</span></h5>
                </div>

                <div className="row">
                    <h5>Price: <span>{order?.fullPrice.amount} {order?.fullPrice.currency}</span></h5>
                </div>

                <div className="row">
                    <h5>Order Status: <span>{order?.orderStatus}</span></h5>
                </div>

            </div>
        </div>
    )
}

export default OrderTracking;

