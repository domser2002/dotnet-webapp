import * as ReactRouterDOM from 'react-router-dom';

export const useNavigate = jest.fn();

export default {
  ...ReactRouterDOM,
  useNavigate,
};