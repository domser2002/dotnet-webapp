import React from 'react';
import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import '@testing-library/jest-dom';
import { FormPage } from "./FormPage.js"; // Ścieżka do twojego komponentu

import { useNavigate } from 'react-router-dom'; // Importujemy nasz mock

jest.mock('react-router-dom', () => require('./mocks/react-router-dom'));

describe('FormPage Component', () => {

    // Test 1
    test('submit button in component', async () => {
        render(<FormPage />);
    
        await screen.findByText(/Submit/i);

        const submitButton = screen.getByText(/Submit/i);
        
        // Sprawdź, czy przycisk jest obecny w dokumencie
        expect(submitButton).toBeInTheDocument();
    });
  
    // Test 2
    test('renders Package Details fields correctly', async () => {
        render(<FormPage />);
        
        // Oczekuj na załadowanie komponentu
        await screen.findByLabelText(/Length/i);
        await screen.findByLabelText(/Width/i);
        await screen.findByLabelText(/Height/i);
        await screen.findByLabelText(/Weight/i);
        
        // Sprawdź, czy pola Package Details są renderowane poprawnie
        const lengthField = screen.getByLabelText(/Length/i);
        const widthField = screen.getByLabelText(/Width/i);
        const heightField = screen.getByLabelText(/Height/i);
        const weightField = screen.getByLabelText(/Weight/i);
        
        expect(lengthField).toBeInTheDocument();
        expect(widthField).toBeInTheDocument();
        expect(heightField).toBeInTheDocument();
        expect(weightField).toBeInTheDocument();
        });

    // Test 3
    test('renders Source Address fields correctly', async () => {
        render(<FormPage />);
        
        // Oczekuj na załadowanie komponentu
        await screen.findAllByLabelText(/Source Street/i);
        await screen.findAllByLabelText(/Source Street Number/i);
        await screen.findAllByLabelText(/Source Flat Number/i);
        await screen.findAllByLabelText(/Source Postal Code/i);
        await screen.findAllByLabelText(/Source City/i);

        // Sprawdź, czy pola Package Details są renderowane poprawnie
        const streetField = screen.getAllByLabelText(/Source Street/i);
        const streetNumberField = screen.getAllByLabelText(/Source Street Number/i);
        const flatNumberField = screen.getAllByLabelText(/Source Flat Number/i);
        const postalCodeField = screen.getAllByLabelText(/Source Postal Code/i);
        const cityField = screen.getAllByLabelText(/Source City/i);
        
        expect(streetField[0]).toBeInTheDocument();
        expect(streetNumberField[0]).toBeInTheDocument();
        expect(flatNumberField[0]).toBeInTheDocument();
        expect(postalCodeField[0]).toBeInTheDocument();
        expect(cityField[0]).toBeInTheDocument();
        });
  
    // Test 4
    test('renders Destination Address fields correctly', async () => {
        render(<FormPage />);
        
        // Oczekuj na załadowanie komponentu
        await screen.findAllByLabelText(/Destination Street/i);
        await screen.findAllByLabelText(/Destination Street Number/i);
        await screen.findAllByLabelText(/Destination Flat Number/i);
        await screen.findAllByLabelText(/Destination Postal Code/i);
        await screen.findAllByLabelText(/Destination City/i);

        // Sprawdź, czy pola Package Details są renderowane poprawnie
        const streetField = screen.getAllByLabelText(/Destination Street/i);
        const streetNumberField = screen.getAllByLabelText(/Destination Street Number/i);
        const flatNumberField = screen.getAllByLabelText(/Destination Flat Number/i);
        const postalCodeField = screen.getAllByLabelText(/Destination Postal Code/i);
        const cityField = screen.getAllByLabelText(/Destination City/i);
        
        expect(streetField[0]).toBeInTheDocument();
        expect(streetNumberField[0]).toBeInTheDocument();
        expect(flatNumberField[0]).toBeInTheDocument();
        expect(postalCodeField[0]).toBeInTheDocument();
        expect(cityField[0]).toBeInTheDocument();
        });

    // Test 5
    test('renders FormPage component', () => {
        render(<FormPage />);
      
          const submitButton = screen.getByText('Submit');
          expect(submitButton).toBeInTheDocument();
      
      });

      // Test 6
      test('renders Priority dropdown on the screen', () => {
        // Render your component
        render(<FormPage />);
      
        // Find the Priority dropdown by its label text
        const priorityDropdown = screen.getByRole('combobox', { label: 'Priority' });
      
        // Assert that the Priority dropdown is in the document
        expect(priorityDropdown).toBeInTheDocument();
      });

      // Test 7
      test('renders Delivery at weekend checkbox', () => {
        // Render your component
        render(<FormPage />);
      
        // Find the checkbox by its label text
        const deliveryCheckbox = screen.getByLabelText('Delivery at weekend');
      
        // Assert that the checkbox exists
        expect(deliveryCheckbox).toBeInTheDocument();
      });
      
  });
