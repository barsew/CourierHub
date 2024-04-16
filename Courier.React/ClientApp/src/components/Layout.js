import React, { Component } from 'react';
import NavMenu from './NavMenu';
import { Box, styled } from '@mui/material';

const StyledBox = styled(Box)({
    width: '100%',
    height: 'calc(100vh - 70px)',
    position: 'relative',
    background: 'linear-gradient(#755139, #feedca)',
});
export class Layout extends Component {
    render() {
        return (
            <Box>
                <NavMenu />
                <StyledBox tag="main" >
                    {this.props.children}
                </StyledBox>
            </Box>
        );
    }
}
