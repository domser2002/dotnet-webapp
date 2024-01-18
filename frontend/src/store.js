import { create } from 'zustand';

export const useStore = create((set) => ({
    SourceStreet: "",
    SourceStreetNumber: "",
    SourceFlatNumber: "",
    SourcePostalCode: "",
    SourceCity: "",

    DestinationStreet: "",
    DestinationStreetNumber: "",
    DestinationFlatNumber: "",
    DestinationPostalCode: "",
    DestinationCity: "",

    DeliveryAtWeekend: true,

    Priority: 0,

    Length: "",
    Width: "",
    Height: "",
    Weight: "",

    DateFrom: null,
    DateTo: null,


    offerId: 0,


    PersonaData: "",
    Email: "",
    CompanyName: "",
    OwnerSourceStreet: "",
    OwnerSourceStreetNumber: "",
    OwnerSourceFlatNumber: "",
    OwnerSourcePostalCode: "",
    OwnerSourceCity: "",

    setSourceStreet: (newValue) => set({ SourceStreet: newValue }),
    setSourceStreetNumber: (newValue) => set({ SourceStreetNumber: newValue }),
    setSourceFlatNumber: (newValue) => set({ SourceFlatNumber: newValue }),
    setSourcePostalCode: (newValue) => set({ SourcePostalCode: newValue }),
    setSourceCity: (newValue) => set({ SourceCity: newValue }),
    setDestinationStreet: (newValue) => set({ DestinationStreet: newValue }),
    setDestinationStreetNumber: (newValue) => set({ DestinationStreetNumber: newValue }),
    setDestinationFlatNumber: (newValue) => set({ DestinationFlatNumber: newValue }),
    setDestinationPostalCode: (newValue) => set({ DestinationPostalCode: newValue }),
    setDestinationCity: (newValue) => set({ DestinationCity: newValue }),
    setDeliveryAtWeekend: (newValue) => set({ DeliveryAtWeekend: newValue }),
    setPriority: (newValue) => set({ Priority: newValue }),
    setLength: (newValue) => set({ Length: newValue }),
    setWidth: (newValue) => set({ Width: newValue }),
    setHeight: (newValue) => set({ Height: newValue }),
    setWeight: (newValue) => set({ Weight: newValue }),
    setDateFrom: (newValue) => set({ DateFrom: newValue }),
    setDateTo: (newValue) => set({ DateTo: newValue }),
    setOfferId: (newValue) => set({ offerId: newValue }),

    setPersonaData: (newValue) => set({ PersonaData: newValue }),
    setEmail: (newValue) => set({ Email: newValue }),
    setCompanyName: (newValue) => set({ CompanyName: newValue }),
    setOwnerSourceStreet: (newValue) => set({ OwnerSourceStreet: newValue }),
    setOwnerSourceStreetNumber: (newValue) => set({ OwnerSourceStreetNumber: newValue }),
    setOwnerSourceFlatNumber: (newValue) => set({ OwnerSourceFlatNumber: newValue }),
    setOwnerSourcePostalCode: (newValue) => set({ OwnerSourcePostalCode: newValue }),
    setOwnerSourceCity: (newValue) => set({ OwnerSourceCity: newValue }),
}));
