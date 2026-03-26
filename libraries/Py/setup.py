"""
setup.py – build/install configuration for the gpm-login-global Python library.
"""

from setuptools import setup, find_packages

setup(
    name="gpm-login-global",
    version="1.0.0",
    description="Official Python client library for the GPMLogin Global Local API",
    author="GPMLogin",
    python_requires=">=3.10",
    packages=find_packages(),
    install_requires=[
        "requests>=2.28.0",
    ],
    classifiers=[
        "Programming Language :: Python :: 3",
        "Programming Language :: Python :: 3.10",
        "Programming Language :: Python :: 3.11",
        "Programming Language :: Python :: 3.12",
        "License :: OSI Approved :: MIT License",
        "Operating System :: OS Independent",
        "Intended Audience :: Developers",
        "Topic :: Software Development :: Libraries",
    ],
)
