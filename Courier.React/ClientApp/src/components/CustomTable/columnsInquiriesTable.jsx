import { format } from 'date-fns';
import { ColumnFilter } from './columnFilter';

export const COLUMNS_INQUIRIES = [
    {
        Header: "#",
        Footer: "#",
        Cell: ({ row }) => row.index + 1
    },
    //{
    //    Header: 'Id',
    //    Footer: 'Id',
    //    accessor: 'id',
    //    Filter: ColumnFilter,
    //},
    {
        Header: 'Weight',
        Footer: 'Weight',
        accessor: 'weight',
        Filter: ColumnFilter,
    },
    {
        Header: 'Width',
        Footer: 'Width',
        accessor: 'width',
        Filter: ColumnFilter,
    },
    {
        Header: 'Length',
        Footer: 'Length',
        accessor: 'length',
        Filter: ColumnFilter,
    },
    {
        Header: 'Length',
        Footer: 'Length',
        accessor: 'height',
        Filter: ColumnFilter,
    },
    {
        Header: 'Source Address',
        Footer: 'Source Address',
        accessor: 'sourceAddress.city',
        Filter: ColumnFilter,
    },
    {
        Header: 'Destination Address',
        Footer: 'Destination Address',
        accessor: 'destinationAddress.city',
        Filter: ColumnFilter,
    },
    {
        Header: 'Pickup Date',
        Footer: 'Pickup Date',
        accessor: 'pickupDate',
        Cell: ({ value }) => { return format(new Date(value), 'dd/MM/yyyy'); },
        Filter: ColumnFilter,
    },
    {
        Header: 'Delivery Date',
        Footer: 'Delivery Date',
        accessor: 'deliveryDate',
        Cell: ({ value }) => { return format(new Date(value), 'dd/MM/yyyy'); },
        Filter: ColumnFilter,
    },
    //{
    //    Header: '',
    //    Footer: '',
    //    accessor: 'id',
    //    Filter: false,
    //    Cell: ({ row }) => (
    //        <button onClick={() => { console.log(row.original.id) }}>
    //            Show details
    //        </button>
    //    )
    //}
]
