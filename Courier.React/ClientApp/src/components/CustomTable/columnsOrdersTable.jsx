import { format } from 'date-fns';
import { ColumnFilter } from './columnFilter';
import { Link } from "react-router-dom";
export const COLUMNS_ORDERS = [
    {
        Header: "#",
        Footer: "#",
        Cell: ({ row }) => row.index + 1
    },
    {
        Header: 'Order Number',
        Footer: 'Order Number',
        accessor: 'orderId',
        Filter: ColumnFilter,
    },
    {
        Header: 'Order Status',
        Footer: 'Order Status',
        accessor: 'orderStatus',
        Filter: ColumnFilter,
    },
    {
        Header: 'Delivery Date',
        Footer: 'Delivery Date',
        accessor: 'deliveryDate',
        Cell: ({ value }) => { return format(new Date(value), 'dd/MM/yyyy'); },
        Filter: ColumnFilter,
    },
    {
        Header: 'Order Date',
        Footer: 'Order Date',
        accessor: 'orderDate',
        Cell: ({ value }) => { return format(new Date(value), 'dd/MM/yyyy'); },
        Filter: ColumnFilter,
    },
    {
        Header: '',
        Footer: '',
        accessor: 'id',
        Filter: false,
        Cell: ({ row }) => (
            <Link to={`details/${row.original.orderId}`} state={{ order: row.original }}>
                Show details
            </Link>
        )
    }
]
