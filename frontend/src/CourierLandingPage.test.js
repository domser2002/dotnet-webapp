import React from 'react';
import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom';
import { CourierLandingPage } from './CourierLandingPage.js';

import { useNavigate } from 'react-router-dom'; // Importujemy nasz mock

jest.mock('react-router-dom', () => require('./mocks/react-router-dom'));

describe('CourierLandingPage Component', () => {
  test('renders Courier Hub title', () => {
    render(<CourierLandingPage />);
    const titleElement = screen.getByText(/Courier Hub/i);
    expect(titleElement).toBeInTheDocument();
  });

  test('renders for Couriers subtitle', () => {
    render(<CourierLandingPage />);
    const subtitleElement = screen.getByText(/for Couriers/i);
    expect(subtitleElement).toBeInTheDocument();
  });

  test('renders first author', () => {
    render(<CourierLandingPage />);
    const authorsElement = screen.getByText(/Zuzia Wójtowicz/i);
    expect(authorsElement).toBeInTheDocument();
  });

  test('renders second author', () => {
    render(<CourierLandingPage />);
    const authorsElement = screen.getByText(/Dominik Seredyn/i);
    expect(authorsElement).toBeInTheDocument();
  });

  test('renders second author', () => {
    render(<CourierLandingPage />);
    const authorsElement = screen.getByText(/Mati Chmurzyński/i);
    expect(authorsElement).toBeInTheDocument();
  });

  test('renders an image with alt text "wydra"', () => {
    render(<CourierLandingPage />);
    const imageElement = screen.getByAltText('wydra');
    expect(imageElement).toBeInTheDocument();
  });
});
