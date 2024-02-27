/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './**/*.html',
        './**/*.razor',
        '../RegistrationPortal.Client/**/*.html',
        '../RegistrationPortal.Client/**/*.razor',
        '../../RegistrationPortal.Shared/**/*.html',
        '../../RegistrationPortal.Shared/**/*.razor'
    ],
  theme: {
    extend: {},
  },
    plugins: [
        require('@tailwindcss/forms'),
    ],
}
