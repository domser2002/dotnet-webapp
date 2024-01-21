import React from 'react';
import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom';
import { MemoryRouter } from 'react-router-dom';
import { useAuth0 } from '@auth0/auth0-react';
import { RoleButton } from './RoleButton';

// Mockowanie useAuth0 i getIdTokenClaims
jest.mock('@auth0/auth0-react');

describe('RoleButton Component', () => {
  test('renders "Send Package" button for authenticated user with role: "User"', () => {
    // Ustawienie wartości isAuthenticated na false
    useAuth0.mockReturnValue({
        isAuthenticated: true,
        getIdTokenClaims: jest.fn().mockResolvedValue({ role: ['User'] }),
      });

    render(
      <MemoryRouter>
        <RoleButton />
      </MemoryRouter>
    );

    // Sprawdzenie, czy renderuje przycisk "Send Package"
    const sendPackageButton = screen.getByText('Send Package');
    expect(sendPackageButton).toBeInTheDocument();
  });

  test('renders "Send Package" button for non-authenticated user', () => {
    // Ustawienie wartości isAuthenticated na false
    useAuth0.mockReturnValue({
        isAuthenticated: false,
      });

    render(
      <MemoryRouter>
        <RoleButton />
      </MemoryRouter>
    );

    // Sprawdzenie, czy renderuje przycisk "Send Package"
    const sendPackageButton = screen.getByText('Send Package');
    expect(sendPackageButton).toBeInTheDocument();
  });

  test('renders "Courier panel" button for authenticated user with Courier role', async () => {
    // Ustawienie wartości isAuthenticated na true i role na 'Courier'
    useAuth0.mockReturnValue({
        isAuthenticated: true,
        getIdTokenClaims: jest.fn().mockResolvedValue({ role: ['Courier'] }),
      });

    render(
      <MemoryRouter>
        <RoleButton />
      </MemoryRouter>
    );
    await screen.findAllByText(/Courier panel/i);
    // Sprawdzenie, czy renderuje przycisk "Courier panel"
    const courierPanelButton = screen.getAllByText(/Courier panel/i);
    expect(courierPanelButton[0]).toBeInTheDocument();
  });

  test('renders "Office worker panel" and "Company offers" buttons for authenticated OfficeWorker role', async () => {
    // Ustawienie wartości isAuthenticated na true i role na 'OfficeWorker'
    useAuth0.mockReturnValue({
        isAuthenticated: true,
        getIdTokenClaims: jest.fn().mockResolvedValue({ role: ['OfficeWorker'] }),
      });

    render(
      <MemoryRouter>
        <RoleButton />
      </MemoryRouter>
    );

    await screen.findByText(/Office worker panel/i);
    // Sprawdzenie, czy renderuje przyciski "Office worker panel" i "Company offers"
    const officeWorkerPanelButton = screen.getAllByText(/Office worker panel/i);
    const companyOffersButton = screen.getAllByText(/Office worker panel/i);

    expect(officeWorkerPanelButton[0]).toBeInTheDocument();
    expect(companyOffersButton[0]).toBeInTheDocument();
  });
});
