export interface Language {
  code: string;
  label: string;
  flag: string;
}

export const languages: Language[] = [
  { code: 'en', label: 'English', flag: '/images/flag-uk.png' },
  { code: 'vi', label: 'Vietnam', flag: '/images/flag-vietnam.png' },
];
