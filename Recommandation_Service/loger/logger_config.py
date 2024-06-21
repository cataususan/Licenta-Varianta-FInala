import logging
import logging.handlers

def setup_logging():
    # Set up basic configuration
    logging.basicConfig(level=logging.DEBUG,
                        format='%(asctime)s - %(levelname)s - %(message)s',
                        datefmt='%Y-%m-%d %H:%M:%S')

    # File handler which logs even debug messages
    fh = logging.FileHandler('myapp.log')
    fh.setLevel(logging.DEBUG)
    fh.setFormatter(logging.Formatter('%(asctime)s - %(levelname)s - %(message)s'))

    # Add the file handler to the root logger
    logging.getLogger('').addHandler(fh)
