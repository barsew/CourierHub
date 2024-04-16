import { useAuth0 } from "@auth0/auth0-react";
import { useEffect, useState } from "react";
import { useLocation, useParams, useNavigate } from 'react-router-dom'

const OrderDetails = () => {

    const navigate = useNavigate();
    const orderId = useParams();
    const location = useLocation();
    const data = location.state;
    const [order, setOrder] = useState(data.order);
    const { user, isAuthenticated } = useAuth0();
    const { getIdTokenClaims } = useAuth0();
    const [selectedStatus, setSelectedStatus] = useState('Pending');

    const officeWorkerDropDownItems = [
        { orderStatus: "Pending", index: 0 },
        { orderStatus: "Accepted", index: 1 },
        { orderStatus: "Rejected", index: 2 }];

    const courierDropDownItems = [
        { orderStatus: "Received", index: 3 },
        { orderStatus: "Delivered", index: 4 },
        { orderStatus: "CannotDeliver", index: 5 }];

    const adminDropDownItems = [...officeWorkerDropDownItems, ...courierDropDownItems];

    var currentDropDownItems;

    const isChangeStatusActive = user.role[0] == "Admin" || user.role[0] == "OfficeWorker" || user.role[0] == "Courier";

    if (user.role[0] == "Admin")
        currentDropDownItems = [...adminDropDownItems];
    else if (user.role[0] == "OfficeWorker")
        currentDropDownItems = [...officeWorkerDropDownItems];
    else if (user.role[0] == "Courier")
        currentDropDownItems = [...courierDropDownItems];

    const changeOrderStatus = async () => {
        if (!selectedStatus) {
            alert('Select a status first');
            return;
        }

        const status = currentDropDownItems.find(item => item.orderStatus == selectedStatus);

        try {
            const claims = await getIdTokenClaims();
            const id_token = claims.__raw;
            const response = await fetch(`/api2/order/details/${orderId.id}/changestatus`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${id_token}`,
                },
                body: JSON.stringify(status.index),
            });

            if (response.ok) {
                setOrder({ ...order, orderStatus: selectedStatus })
                alert('Status has been changed!');
            } else {
                console.error('Error changing order status:', response.statusText);
            }
        } catch (error) {
            console.error('Error changing order status:', error);
        }
    };

    return (
        <> {isAuthenticated &&
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
                    {isChangeStatusActive &&
                        <div>
                            <select value={selectedStatus} onChange={e => {
                                setSelectedStatus(e.target.value);
                            }}>
                                {
                                    currentDropDownItems.map((opt, index) => 
                                        <option key="index">
                                            {opt.orderStatus}
                                        </option>)
                                }
                            </select>
                            <button onClick={changeOrderStatus}>Change Status</button>
                        </div>
                    }
                </div>
                <button onClick={() => navigate(-1) }>Go back</button>
            </div>
        }
        </>
    )
}

export default OrderDetails;

