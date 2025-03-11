import containerQueries from '@tailwindcss/container-queries';
import forms from '@tailwindcss/forms';
import typography from '@tailwindcss/typography';
import type { Config } from 'tailwindcss';

const DARK = {
  PRIMARY: "#0e1621",
  SECONDARY: "#17212b",
  SECONDARY_HOVER: "#202b36",
  ACCENT: "#2b5278",
  BORDER_PRIMARY: "#101921"
} as const;

const LIGHT = {
  PRIMARY: "#f0f0f0",
  SECONDARY: "#293a4c",
  SECONDARY_HOVER: "#3a4c5f",
  ACCENT: "#419fd9",
  BORDER_PRIMARY: "#e7e7e7"
} as const;

const TEXT_SIDEBAR = "#8393a3";

export default {
  content: ['./src/**/*.{html,js,svelte,ts}'],

  theme: {
    extend: {
      colors: {
        primary: LIGHT.PRIMARY,
        secondary: LIGHT.SECONDARY,
        accent: LIGHT.ACCENT,
        hover: LIGHT.SECONDARY_HOVER,
        dark: {
          primary: DARK.PRIMARY,
          secondary: DARK.SECONDARY,
          accent: DARK.ACCENT,
          hover: DARK.SECONDARY_HOVER,
        }
      },
      textColor: {
        sidebar: TEXT_SIDEBAR,
        accent: LIGHT.ACCENT,
        dark: {
          accent: DARK.ACCENT,
        }
      },
      borderColor: {
        primary: LIGHT.BORDER_PRIMARY,
        dark: {
          primary: DARK.BORDER_PRIMARY,
        }
      }
    },
  
  },
  darkMode: 'class',
  plugins: [typography, forms, containerQueries]
} satisfies Config;
